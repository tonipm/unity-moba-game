using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 lookDirection;
    private bool canMove;
    private Vector3 input;
    private Rigidbody rb;
    private Vector3 tmpEulerRot;
    private NetManager netManager;
    private Unidad jugador;
    private Animation animations;
    public bool execAnimationIdle;

    // Use this for initialization
    void Start ()
    {
        this.execAnimationIdle = true;
        this.netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();
        this.canMove = true;
        this.input = Vector3.zero;
        this.rb = GetComponent<Rigidbody>();
        this.lookDirection = Vector3.zero;
        this.tmpEulerRot = Vector3.zero;
        this.animations = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.canMove)
        {
            this.input.Set(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
                );
            this.tmpEulerRot.y = Quaternion.LookRotation(this.lookDirection).eulerAngles.y;
            this.transform.rotation = Quaternion.Euler(this.tmpEulerRot);
        }
    }

    private void FixedUpdate()
    {
        if (this.input != Vector3.zero)
        {
            this.jugador = netManager.GetJugador();
            this.rb.velocity = input * this.jugador.GetMovementSpeed();
            if (execAnimationIdle)
            {
                //this.animations.Play("run");
            }
        }
        else
        {
            if (execAnimationIdle)
            {
                //this.animations.Play("idle");
            }
        }
    }
    public void SetCanMove(bool _canMove)
    {
        this.canMove = _canMove;
    }
}
