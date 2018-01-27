using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {
	public static GameController instance;

	public List<Player> players;

	public Text timerText;
	public Text countdownText;
	public Text[] playersScoreText;
	public Color[] teamColor;
	public int gameDuration = 180;

	private float timer = -4f;
	private Player lastPlayerWithBall;
	private int combo = 1;
	private int[] teamNumbers = { 1, 1, 2, 2 };

	void Start() {
		if (instance == null) {
			instance = this;
		}
		InitTeam ();
	}

	void Update() {
		SetTimer ();
	}

	void SetTimer() {
		timer += Time.deltaTime;
		if (timer < 0) {
			timerText.text = "";
			countdownText.text = Mathf.FloorToInt (Mathf.Abs (timer)).ToString ();
		} else if (timer >= gameDuration) {
			GameFinished ();
		} else {
			timerText.text =  Mathf.FloorToInt (timer).ToString ();
			countdownText.text ="";
		}
	}

	void InitTeam() {
		List<int> teams = new List<int>(teamNumbers);

		for (int i = 0; i < players.Count; i++) {
			int rand = Mathf.FloorToInt(Random.Range (0, players.Count - i));
			players [i].team = teams [rand];
			teams.Remove (teams [rand]);
		}
	}

	void SwapTeam() {
	}

	public void PlayerGetBall(Player player) {
		if (player.id != lastPlayerWithBall.id) {
			if (player.team == lastPlayerWithBall.team) {
				player.ScorePoint (combo);
				combo++;
			} else {
				combo = 1;
			}
			lastPlayerWithBall = player;
		}
	}

	void GameFinished() {
		Debug.Log ("GameFinished");
	}
}