using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour {
	public Text enemiesLeft;
	public Text gameEnd;
	
	// Update is called once per frame
	void Update () {
		enemiesLeft.text = "Enemies Left: " + transform.childCount;
		if (transform.childCount == 0) {
			gameEnd.text = "You WIN!!";
		}
	}
}
