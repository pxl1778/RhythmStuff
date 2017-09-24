using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public ParticleSystem movePS;
    public ParticleSystem drumPS;
    public float moveSpeed;

    private Staff staff;
    private bool moving = false;
    private bool attacking = false;
    private Rigidbody2D rb;

    void Start () {
        staff = new Staff(this);
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        HandleMovementInput();
        HandleButtonInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.UP);
                if(rb.velocity.y < moveSpeed * 2)
                {
                    rb.velocity = new Vector2(0, moveSpeed + rb.velocity.y);
                }
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.LEFT);
                if (rb.velocity.x > -moveSpeed * 2)
                {
                    rb.velocity = new Vector2(-moveSpeed + rb.velocity.x, 0);
                }
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.RIGHT);
                if (rb.velocity.x < moveSpeed * 2)
                {
                    rb.velocity = new Vector2(moveSpeed + rb.velocity.x, 0);
                }
                GameObject.Instantiate(movePS, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.DOWN);
                //StartCoroutine(Move(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - 1, 0)));
                if (rb.velocity.y > -moveSpeed * 2)
                {
                    rb.velocity = new Vector2(0, -moveSpeed + rb.velocity.y);
                }
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
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.SQUARE);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.CIRCLE);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Border border = GameObject.Find("border").GetComponent<Border>();
            if (border.isCorrect())
            {
                staff.addBeat(BEAT.X);
                GameObject.Instantiate(drumPS, new Vector3(this.transform.position.x, this.transform.position.y - 2.5f, 0), Quaternion.identity, this.transform);
            }
        }
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
