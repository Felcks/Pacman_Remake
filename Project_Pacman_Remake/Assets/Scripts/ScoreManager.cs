using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public int score;
	public int highScore;

	public Text scoreText;
	public Text highScoreText;

	void Start()
	{
		this.score = PlayerPrefs.GetInt ("CurrentScore");
		this.scoreText = GameObject.Find ("Score").GetComponent<Text> ();

		this.highScore = PlayerPrefs.GetInt ("HighScore");
		this.highScoreText = GameObject.Find ("HighScore").GetComponent<Text> ();
		this.highScoreText.text = "HighScore " + this.highScore;
	}

	public void AddScore()
	{
		this.score += 10;
		this.scoreText.text = "Score " + this.score;
	}
}
