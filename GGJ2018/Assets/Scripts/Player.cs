﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int id;
	public Color color;
	[HideInInspector]
	public int team;
	public bool canPickup = true;

	private int score;
	[HideInInspector]
	public bool hasBall;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ball" && canPickup) {
			hasBall = true;
			GameController.instance.PlayerGetBall (this);
			Destroy(other.gameObject);
		} else if (!canPickup) {
			canPickup = true;
		}
	}

	public void ScorePoint(int point) {
		score += point;
	}
}
