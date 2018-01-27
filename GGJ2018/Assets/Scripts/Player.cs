using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int id;
	public Color color;
	[HideInInspector]
	public int team;

	private int score;
	private bool hasBall;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ball") {
			hasBall = true;
			GameController.instance.PlayerGetBall (this);
		}
	}

	public void ScorePoint(int point) {
		score += point;
	}
}
