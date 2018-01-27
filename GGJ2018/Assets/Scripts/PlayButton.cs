using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	// Starting the game
	public void StartGame() {
		SceneManager.LoadScene ("Game");
	}
}
