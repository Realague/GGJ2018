using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {
	public float duration;

	private float time = 0;
	private Image image;

	void Awake() {
		image = GetComponent<Image> ();
	}

	void Update() {
		if (time == 0) {
			return;
		}
		time -= Time.deltaTime;
		if (time < 0) {
			time = 0;
			image.color = new Color (1, 1, 1, 0);
		} else {
			image.color = new Color (1, 1, 1, (time / duration));
		}
	}

	public void GetFlashed() {
		time = duration;
	}

}
