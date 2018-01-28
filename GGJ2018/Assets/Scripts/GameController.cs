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
	public Image[] playersPanel;
	public Color[] teamColors;
	public float gameDuration = 180;

	private float timer = -4f;
	[HideInInspector]
	public Player lastPlayerWithBall = null;
	private int combo = 1;
	private List<int[]> teamPossibilities = new List<int[]>();
	private int actualTeamConfig;

	void Start() {
		if (instance == null) {
			instance = this;
		}
		InitTeam ();
		StartCoroutine (SwapTeam());
	}

	void Update() {
		SetTimer ();
		UpdateScore();
	}

	void UpdateScore() {
		foreach (Player player in players) {
			int zero = 4 - player.score.ToString().Length;
			string score = "";
			for (int i = 0; i != zero; i++) {
				score += "0";
			}
			score += player.score.ToString();
			playersScoreText[player.id - 1].text = score;
		}
	}

	void SetTimer() {
		timer += Time.deltaTime;
		gameDuration -= Time.deltaTime;
		if (timer < 0) {
			timerText.text = "";
			countdownText.text = Mathf.FloorToInt (Mathf.Abs (timer)).ToString ();
		} else if (gameDuration + 5 <= 0) {
			GameFinished ();
		} else {
			countdownText.text = "";
			timerText.text = Mathf.FloorToInt (gameDuration + 5).ToString ();
		}
	}

	void InitTeam() {
		teamPossibilities.Add (new int[]{1, 1, 2, 2});
		teamPossibilities.Add (new int[]{2, 2, 1, 1});
		teamPossibilities.Add (new int[]{1, 2, 2, 1});
		teamPossibilities.Add (new int[]{2, 1, 1, 2});
		teamPossibilities.Add (new int[]{1, 2, 1, 2});
		teamPossibilities.Add (new int[]{2, 1, 2, 1});

		actualTeamConfig = Mathf.FloorToInt(Random.Range (0, 3));
		int randColor = Mathf.FloorToInt(Random.Range (0, 2));

		for (int i = 0; i < players.Count; i++) {
			players [i].team = teamPossibilities [actualTeamConfig * 2 + randColor][i];
			playersPanel [i].color = teamColors [players [i].team - 1];
		}
	}

	IEnumerator SwapTeam() {
		yield return new WaitForSeconds (4);
		while (true) {
			yield return new WaitForSeconds(30);
			StartCoroutine (SwapTimeDisplay());
			int randTeam = Mathf.FloorToInt(Random.Range (0, 2));
			int randColor = Mathf.FloorToInt(Random.Range (0, 2));

			for (int i = 0; i < 3; i++) {
				if (actualTeamConfig == i) {
					i++;
				}
				if (randTeam == 0) {
					for (int j = 0; j < players.Count; j++) {
						players [j].team = teamPossibilities [i * 2 + randColor][j];
						playersPanel [j].color = teamColors [players [j].team - 1];
					}
					actualTeamConfig = i;
				}
				randTeam--;
			}
		}
	}

	IEnumerator SwapTimeDisplay() {
		yield return null;
	}

	public void PlayerGetBall(Player player) {
		if (lastPlayerWithBall && player.id != lastPlayerWithBall.id) {
			if (player.team == lastPlayerWithBall.team) {
				player.ScorePoint (combo);
				lastPlayerWithBall.ScorePoint (combo);
				combo++;
			} else {
				combo = 1;
				player.ScorePoint (combo);
			}
		}
		lastPlayerWithBall = player;
	}

	void GameFinished() {
		StopCoroutine ("SwapTeam");
		Debug.Log ("GameFinished");
	}

}