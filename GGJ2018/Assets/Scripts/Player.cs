using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public Text pointsWonText;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ball" && canPickup) {
			hasBall = true;
			GameController.instance.PlayerGetBall (this);
			Destroy(other.gameObject);
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
			StartCoroutine(DisplayWonPoints (point));
		}
	}

	IEnumerator DisplayWonPoints(int point) {
		pointsWonText.text = "+" + point.ToString ();
		var tmpColor = GameController.instance.teamColors [team - 1];
		pointsWonText.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 1);
		yield return new WaitForSeconds (0.5f);
		pointsWonText.text = "";
	}
}
