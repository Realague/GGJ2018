using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Animator myAnimator;
	[SerializeField]
	private float speed = 4f;
	private bool facingRight;
	private Rigidbody2D rb;
	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;
	[SerializeField]
	private float jumpForce;
	private bool slide;
	private Player player;
	private bool punch;
	public float cdPunch;
	private bool canPunch = true;
	public float punchForce = 200f;

	void Start () {
		facingRight = true;
		player = GetComponent<Player> ();
		rb = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}

	void Update() {
		myAnimator.SetBool("Ball", player.hasBall);
		HandleInput ();
	}

	void FixedUpdate () {
		isGrounded = isGrounded2();
		float horizontal = Input.GetAxis ("Horizontal" + player.id);
		HandleMovement (horizontal);
		Flip (horizontal);
		ResetValues ();
	}

	private void HandleInput() {
		if (Input.GetButtonDown ("Cross" + player.id)) {
			jump = true;
		}
		if (Input.GetButtonDown ("Square" + player.id)) {
			if (!player.hasBall && canPunch) {
				slide = true;
			}
		}
	}

	private void HandleMovement(float horizontal) {
		transform.Translate(new Vector2(horizontal, 0) * Time.deltaTime * speed);
		myAnimator.SetFloat ("Speed", Mathf.Abs(horizontal));
		if (isGrounded && jump) {
			isGrounded = false;
			rb.AddForce (new Vector2 (0, jumpForce));
		}
		if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
			myAnimator.SetBool ("slide", true);
			StartCoroutine (Punch());
		} else if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
			myAnimator.SetBool ("slide", false);
		}
	}
		

	private void Flip(float horizontal) {
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	private void ResetValues() {
		jump = false;
		slide = false;
	}

	private bool isGrounded2() {
		if (rb.velocity.y <= 0) {
			myAnimator.SetBool("Jump", false);
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders[i].gameObject != gameObject) {
						return (true);
					}
				}
			}
		} else if (!isGrounded) {
			myAnimator.SetBool("Jump", true);
		}
	return (false);
	}

	IEnumerator Punch() {
		canPunch = false;
		yield return new WaitForSeconds (0.125f);
		punch = true;
		yield return new WaitForSeconds (0.125f);
		punch = false;
		yield return new WaitForSeconds (cdPunch);
		canPunch = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (punch) {
			if (other.tag == "Player") {
				other.GetComponent<PlayerController>().ReceivePunch (this);
			}
			punch = false;
		}
	}

	public void ReceivePunch(PlayerController other) {
		if (player.hasBall) {
			GetComponent<LaunchBall>().ReleaseBall();
		}
		rb.AddForce ((transform.position - other.transform.position) * punchForce);
	}
}
