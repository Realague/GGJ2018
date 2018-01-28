using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayPlayer : MonoBehaviour {

	private Image image;
	[SerializeField]
	private int playerIndex;

	private void Start() {
        image = GetComponent<Image>();
	}

    private void Update() {
        image.sprite = GameController.instance.players[playerIndex].GetComponent<SpriteRenderer>().sprite;
    }
}
