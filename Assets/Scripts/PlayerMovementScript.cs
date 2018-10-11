using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

	public float walkSpeed = 3f;
	public float jumpPower = 300f;
    public float jumpNum;
    public float health = 100f;
    public bool direc = true;
    public bool alive;

	public LayerMask groundMask;
	public float groundRadius = 0.1f;

	private Rigidbody2D theRigidbody;
	private Transform groundCheckLeft;
	private Transform groundCheckRight;
    private Transform punchObj;
    private Transform punchLObj;

    private Vector3 resetPosition;
    private Animator anim;

    private Text healthText;

	// Use this for initialization
	void Start () {
        resetPosition = transform.position;
		theRigidbody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		groundCheckLeft = transform.Find ("LeftGround");
        groundCheckRight = transform.Find("RightGround");
        punchObj = transform.Find("PunchingPlayer");
        punchLObj = transform.Find("PunchingLPlayer");
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        alive = true;
    }

	// Update is called once per frame
	void Update () {
        if (alive)
        {
            //Walking
            float inputX = Input.GetAxis("Horizontal1");
            theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
            if (theRigidbody.velocity.x > 1)
            {
                direc = true;
            }
            if (theRigidbody.velocity.x < 1)
            {
                direc = false;
            }

            //Jumping
            bool grounded = Physics2D.OverlapCircle(groundCheckLeft.position, groundRadius, groundMask) || Physics2D.OverlapCircle(groundCheckRight.position, groundRadius, groundMask);
            bool jumping = Input.GetButtonDown("Jump1");
            if (grounded)
            {
                jumpNum = 1;
            }
            if (grounded && jumping)
            {
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
                theRigidbody.AddForce(new Vector2(0, jumpPower));
            }
            else if (!grounded && jumping)
            {
                if (jumpNum > 0)
                {
                    jumpNum--;
                    theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
                    theRigidbody.AddForce(new Vector2(0, jumpPower));
                }
            }

            //Punching
            bool punching = Input.GetButtonDown("Fire1");
            if (punching && direc)
            {
                punchObj.gameObject.SetActive(true);
            }
            else if (punching && !direc)
            {
                punchLObj.gameObject.SetActive(true);
            }
            if (!punching)
            {
                punchObj.gameObject.SetActive(false);
                punchLObj.gameObject.SetActive(false);
            }

            //UI
            healthText.text = "Health: " + health + "%";

            //Animations
            anim.SetBool("punching", punching);
            anim.SetBool("grounded", grounded);
            anim.SetFloat("speed", theRigidbody.velocity.x);
            anim.SetFloat("grav", Mathf.Abs(theRigidbody.velocity.y));

            //Death
            if (health <= 0f)
            {
                anim.SetBool("dead", true);
                alive = false;
            }
        }
    }

    //Colliding with "Floor"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            transform.position = resetPosition;
            health -= 10f;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Hit"))
        {
            health -= 5f;
        }
    }

}
