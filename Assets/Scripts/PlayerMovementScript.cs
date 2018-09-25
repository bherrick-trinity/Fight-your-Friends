using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

	public float walkSpeed = 3f;
	public float jumpPower = 300f;

	public LayerMask groundMask;
	public float groundRadius = 0.1f;

	private Rigidbody2D theRigidbody;
	private Transform groundCheckLeft;
	private Transform groundCheckRight;

    private Vector3 resetPosition;
    private Animator anim;

    private int jumpNum = 0;
    private Text scoreText;

	// Use this for initialization
	void Start () {
        resetPosition = transform.position;
		theRigidbody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		groundCheckLeft = transform.Find ("LeftGround");
		groundCheckRight = transform.Find ("RightGround");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
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
            jumpNum++;
            scoreText.text = "# of Jumps: " + jumpNum;
		}

        anim.SetFloat("speed", Mathf.Abs(theRigidbody.velocity.x));
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            GetComponent<AudioSource>().Play();
            transform.position = resetPosition;
        }
    }

}
