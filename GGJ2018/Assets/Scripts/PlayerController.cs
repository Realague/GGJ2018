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
			slide = true;
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
		} else if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
//			myAnimator.SetBool ("slide", false);
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
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders[i].gameObject != gameObject) {
						return (true);
					}
				}
			}
		}
	return (false);
	}
}
