using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYSTATE
{
    HIT, IDLE
}

public class Enemy : MonoBehaviour {

    public float health = 3;
    public float maxHealth = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("you hit me");
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Debug.Log("you hit me with projectile");
            health -= 1;
            if(health <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
