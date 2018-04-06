using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public Text scoreText;

	int score;
	float startPosition;

	void Start(){
		startPosition = transform.position.x;
		score = 0;
		scoreText.text = "Score: " + score.ToString();
	}

	void Update () {
		score = (int)(transform.position.x - startPosition);
		scoreText.text = "Score: " + score.ToString();
	}

	public int GetScore(){
		return score;
	}
}
