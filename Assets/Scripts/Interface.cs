﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	private int score;
	private int numEnemies;
	private int numLives;
	private bool lost;
	private bool won;
	private int waveNum = 0;

	public int startLives = 3;
	public Shooter shooter;
	public Text ScoreText, EnemiesText, MagazineText, LivesText, wonText, lostText, WaveText;
	GameObject storageObject;

	void Start () {
		score = 0;
		numLives = 3;
		lost = false;
		won = false;
		storageObject = GameObject.Find ("Persistent Data Storage");
	}

	void Update () {
		//set the text to a '|' for each bullet you can shoot
		int magLeft = shooter.getMagazineLeft ();
		if (magLeft > 0) {
			MagazineText.text = new string ('|', magLeft);
		} else {
			MagazineText.text = "Press R to reload";
		}
		numEnemies = GameObject.FindGameObjectsWithTag ("enemy").Length;
		ScoreText.text = score.ToString ();
		EnemiesText.text = GameObject.FindGameObjectsWithTag ("enemy").Length.ToString();
		LivesText.text = new string ('❤', numLives);
		WaveText.text = "Wave: " + waveNum.ToString ();
		if (won) {
			wonText.enabled = true;
		} else if (lost) {
			lostText.enabled = true;
		}
		storageObject.GetComponent<PersistentScript>().score = score;
		storageObject.GetComponent<PersistentScript>().wave = waveNum;
	}

	public void OnEnemyKill(int scoreAmount, bool playerKill) {
		score += scoreAmount;

		if (numEnemies == 0 && playerKill) {
			won = true;
			stopGame ();
		}
	}


	public void OnEnemySpawn() {
	}

	public void OnNewWave(){
		waveNum++;
	}
	
	public void OnPlayerDeath() {
		if (!won && !lost) {
			numLives -= 1;
			if (numLives == 0) {
				lost = true;
				//stopGame ();
				Application.LoadLevel(2);
			}
		}
	}

	private void stopGame() {
		MonoBehaviour[] components = shooter.GetComponentsInParent<MonoBehaviour>();
		foreach (Component c in components) {
			Destroy (c);
		}
	}
}
