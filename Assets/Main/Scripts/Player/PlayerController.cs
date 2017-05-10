using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 lookDirection;
    private Vector3 input;
    private float speed;
    private Rigidbody rb;
    private Vector3 tmpEulerRot;

	// Use this for initialization
	void Start () {
        this.input = Vector3.zero;
        this.speed = 5;
        this.rb = GetComponent<Rigidbody>();
        this.lookDirection = Vector3.zero;
        this.tmpEulerRot = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        this.input.Set(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
            );
        this.tmpEulerRot.y = Quaternion.LookRotation(this.lookDirection).eulerAngles.y;
        this.transform.rotation = Quaternion.Euler(this.tmpEulerRot);
	}

    private void FixedUpdate()
    {
        if (this.input != Vector3.zero)
        {
            this.rb.velocity = input * this.speed;
        }
    }
}
