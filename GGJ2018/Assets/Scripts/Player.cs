using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int id = 1;
	public Color color;
	[HideInInspector]
	public int team;
	[HideInInspector]
	public bool canPickup = true;

	[HideInInspector]
	public int score;
	[HideInInspector]
	public bool hasBall;
	[HideInInspector]
	public bool ready;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ball" && canPickup) {
			hasBall = true;
			GameController.instance.PlayerGetBall (this);
			Destroy(other.gameObject);
		} else if (!canPickup) {
			canPickup = true;
		}
	}

	void Update() {
		if (GameController.instance.gameDuration < 0) {
			if (Input.GetButtonDown ("Cross" + id)) {
				ready = !ready;
			}
		}
	}

	public void ScorePoint(int point) {
		if (GameController.instance.gameDuration > 0) {
			score += point;
		}
	}
}
