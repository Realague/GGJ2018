using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			PickUp (other);
		}
	}

	void PickUp(Collider player) {
		Player pl = player.GetComponent<Player>();
		int choice = UnityEngine.Random.Range (0, 2);

		if (choice % 2 == 0) {
			pl.ScorePoint (-1);
		} else {
			pl.ScorePoint (1);
		}
	}
}