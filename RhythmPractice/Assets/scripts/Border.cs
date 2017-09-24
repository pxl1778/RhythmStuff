using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {
    private float timePassed = 0;
    private AudioSource audioSource;
    public float maxTime;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if(timePassed >= maxTime)
        {
            timePassed = 0;
            this.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            audioSource.Play();
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (timePassed / maxTime) / 1.5f);
        }
	}

    public bool isCorrect()
    {
        if(maxTime - timePassed <= .1)
        {
            return true;
        }
        if(timePassed <= .1)
        {
            return true;
        }
        return false;
    }
}
