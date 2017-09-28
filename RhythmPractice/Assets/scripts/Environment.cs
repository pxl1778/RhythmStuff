using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = Mathf.RoundToInt((sr.transform.position.y - (sr.bounds.size.y/2))*100f) * -1;
    }
	
	// Update is called once per frame
	void Update () {
    }
}
