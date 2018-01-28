using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour {

	private Vector2 dir = Vector2.zero;
	private bool shoot = false;
	[SerializeField]
	private GameObject ball;
	private GameObject newBall = null;
	private Player player;

	void Start () {
		player = GetComponent<Player> ();
	}
	
	void Update () {
		dir.x = Input.GetAxis("Horizontal" + player.id);
		dir.y = Input.GetAxis("Vertical" + player.id);
		if (dir.magnitude > 1) {
			dir.Normalize();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			LaunchBal(dir);
		}
	}

	void LaunchBal(Vector2 dir) {
		if (player.hasBall) {
			newBall = Instantiate(ball, transform.position, Quaternion.identity);
			player.canPickup = false;
			player.hasBall = false;
			newBall.GetComponent<CircleCollider2D>().isTrigger = true;
			newBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000 * dir.x, 1000 * dir.y));
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (newBall) {
			newBall.GetComponent<CircleCollider2D>().isTrigger = false;
		}
    }

}
