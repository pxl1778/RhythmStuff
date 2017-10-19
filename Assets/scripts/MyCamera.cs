using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MyCamera : MonoBehaviour {

    public Transform tform;
    [SerializeField]
    private Player player;
    [SerializeField]
    private float speed;
    private Vector3 velocity = Vector3.zero;
    private Camera cam1;

	// Use this for initialization
	void Start () {
        cam1 = (Camera)this.gameObject.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 tDisplacement = player.transform.position - this.transform.position;
        velocity.x = tDisplacement.x * speed;
        velocity.y = tDisplacement.y * speed;
        var destination = this.transform.position + velocity;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, speed);
    }
    
}
