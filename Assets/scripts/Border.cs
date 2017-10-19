using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {
    private float timePassed = 0;
    private AudioSource audioSource;
    public float maxTime;
    public SpriteRenderer borderSprite;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        borderSprite = this.transform.GetComponent<SpriteRenderer>();
        borderSprite.size = new Vector2((Camera.main.orthographicSize * 2.0f) * (Screen.width / Screen.height), Camera.main.orthographicSize * 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if(timePassed >= maxTime)
        {
            timePassed = 0;
            borderSprite.color = new Color(1, 1, 1, 1);
            audioSource.Play();
        }
        else
        {
            borderSprite.color = new Color(1, 1, 1, (timePassed / maxTime) / 1.5f);
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
