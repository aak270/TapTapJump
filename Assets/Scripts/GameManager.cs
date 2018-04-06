using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text gameOverText;
	public Text highscoreText;
	public HUDScript scoreManager;
	public GameObject menuUI;
	public GameObject tutorial;
	public GameObject credits;

	private bool endGame;
	int highscore;

	void Start(){
		endGame = false;

		highscore = PlayerPrefs.GetInt ("highscore", highscore);
	}
	
	void Update(){
		if(endGame){
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
				SceneManager.LoadScene("Game");
			}
		}
	}

	public void GameOver(){
		int score = scoreManager.GetScore();
		if(score > highscore){
			highscore = score;
			PlayerPrefs.SetInt ("highscore", highscore);
		}

		highscoreText.text = "Best: " + highscore.ToString();

		endGame = true;
		gameOverText.gameObject.SetActive(true);
		highscoreText.gameObject.SetActive(true);
	}

	public void ShowMenu(){
		Time.timeScale = 0;

		highscoreText.text = "Best: " + highscore.ToString();

		menuUI.SetActive(true);
		highscoreText.gameObject.SetActive(true);
	}

	public void ShowGame(){
		Time.timeScale = 1;

		menuUI.SetActive(false);
		highscoreText.gameObject.SetActive(false);
	}

	public void ShowCredits(){
		credits.SetActive(true);
	}

	public void ShowTutorial(){
		tutorial.SetActive(true);
	}
}
