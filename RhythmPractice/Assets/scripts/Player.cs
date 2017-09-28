using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    IDLE, WALKING, RUNNING, DODGING, ATTACKING
}

public class Player : MonoBehaviour {
    public ParticleSystem movePS;
    public ParticleSystem drumPS;
    public float moveSpeed;
    public float speedMultiplier;
    public float dodgeSpeed;
    public Projectile projectile;

    private float dodgeTimer = 0;
    private float dodgeTimerMax = .1f;
    private float recoveryTimer = 0;
    private float recoveryTimerMax = .5f;
    private bool isRecovering = false;
    private Staff staff;
    private bool moving = false;
    private bool attacking = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Border border;
    private PLAYERSTATE state = PLAYERSTATE.IDLE;

    void Start () {
        staff = new Staff(this);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        border = GameObject.Find("border").GetComponent<Border>();
    }
	
	void Update () {
        HandleStates();
        HandleDodgeInput();
        HandleMovementInput();
        HandleButtonInput();
        sr.sortingOrder = Mathf.RoundToInt((sr.transform.position.y - (sr.bounds.size.y/2))*100f) * -1;
    }

    private void HandleStates()
    {
        switch (state)
        {
            case PLAYERSTATE.DODGING:
                dodgeTimer += Time.deltaTime;
                if (dodgeTimer >= dodgeTimerMax)
                {
                    dodgeTimer = 0;
                    state = PLAYERSTATE.RUNNING;
                }
                break;
        }
    }

    private void HandleMovementInput()
    {
        if (state != PLAYERSTATE.DODGING)
        {
            rb.velocity = Vector2.zero;
            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                speedMultiplier = 1;
            }
            else if (border.isCorrect())
            {
                speedMultiplier = 2;
            }
            rb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal") * speedMultiplier, moveSpeed * Input.GetAxisRaw("Vertical") * speedMultiplier);
        }
    }

    private void HandleDodgeInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.UP);
                state = PLAYERSTATE.DODGING;
                rb.velocity = new Vector2(0, dodgeSpeed);
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, 0), Quaternion.identity, this.transform);
                speedMultiplier = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.LEFT);
                state = PLAYERSTATE.DODGING;
                rb.velocity = new Vector2(-dodgeSpeed, 0);
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
                speedMultiplier = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.RIGHT);
                state = PLAYERSTATE.DODGING;
                rb.velocity = new Vector2(dodgeSpeed, 0);
                speedMultiplier = 2;
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.DOWN);
                state = PLAYERSTATE.DODGING;
                rb.velocity = new Vector2(0, -dodgeSpeed);
                speedMultiplier = 2;
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x, this.transform.position.y - 2.5f, 0), Quaternion.identity, this.transform);
            }
        }
    }

    private void HandleButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.TRIANGLE);
                UseWeapon(BEAT.TRIANGLE);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.SQUARE);
                UseWeapon(BEAT.SQUARE);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.CIRCLE);
                UseWeapon(BEAT.CIRCLE);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.X);
                UseWeapon(BEAT.X);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x, this.transform.position.y - 2.5f, 0), Quaternion.identity, this.transform);
            }
        }
    }

    private void UseWeapon(BEAT beat)
    {
        var tDir = new Vector2();
        switch (beat)
        {
            case BEAT.TRIANGLE:
                tDir = Vector2.up;
                break;
            case BEAT.SQUARE:
                tDir = Vector2.left;
                break;
            case BEAT.CIRCLE:
                tDir = Vector2.right;
                break;
            case BEAT.X:
                tDir = Vector2.down;
                break;
        }
        var tProj = GameObject.Instantiate(projectile, new Vector3(this.transform.position.x + (tDir.x * sr.bounds.size.x), this.transform.position.y + (tDir.y*sr.bounds.size.y), 0), Quaternion.identity);
        tProj.Shoot(tDir);
    }


    /// <summary>
    /// Coroutine that will lerp the movement of the player.
    /// </summary>
    IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        var t = 0.0f;
        while(t < 1.0f)
        {
            t += Time.deltaTime * moveSpeed;
            this.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        moving = false;
    }
}
