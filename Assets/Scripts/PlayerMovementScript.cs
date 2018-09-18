using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public float walkSpeed = 3f;
	public float jumpPower = 300f;

	public LayerMask groundMask;
	public float groundRadius = 0.1f;

	private Rigidbody2D theRigidbody;
	private Transform groundCheckLeft;
	private Transform groundCheckRight;


	// Use this for initialization
	void Start () {
		theRigidbody = GetComponent<Rigidbody2D> ();
		groundCheckLeft = transform.Find ("LeftGround");
		groundCheckRight = transform.Find ("RightGround");
	}

	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
		theRigidbody.velocity = new Vector2 (inputX * walkSpeed, theRigidbody.velocity.y);

		bool grounded = Physics2D.OverlapCircle (groundCheckLeft.position, groundRadius, groundMask) || Physics2D.OverlapCircle(groundCheckRight.position, groundRadius, groundMask);
		bool jumping = Input.GetButtonDown ("Jump");
		if(grounded && jumping) {
			theRigidbody.velocity = new Vector2 (theRigidbody.velocity.x, 0f);
			theRigidbody.AddForce(new Vector2(0, jumpPower));
		}
	}

}
