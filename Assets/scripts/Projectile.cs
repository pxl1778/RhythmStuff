using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector2 direction = Vector2.right;
    public float speed = 3;
    public float lifetime = 1;
    public float timer = 0;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    public void Shoot(Vector2 pDirection)
    {
        if (rb)
        {
            rb.velocity = pDirection * speed;
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = pDirection * speed;
        }
    }
}
