using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

	public Vector3 center;
	public GameObject enemyTracker;
	public GameObject playerTracker;

	private ArrayList enemyList;

	void Start() {
		playerTracker = Instantiate (playerTracker) as GameObject;
		playerTracker.transform.parent = gameObject.transform;
		enemyList = new ArrayList ();
	}

	void Update () {
		playerTracker.transform.localPosition = center + GameObject.FindGameObjectWithTag ("Player").transform.position;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		if (enemyList.Count > enemies.GetLength (0)) {
			for (int i = enemyList.Count - 1; i >= enemies.GetLength(0) - 1; ++i) {
				Destroy ((GameObject)enemyList[i]);
				enemyList.RemoveAt(i);
			}
		}

		for (int i = 0; i < enemies.GetLength(0); ++i) {
			if(i >= enemyList.Count) {
				enemyList.Add (Instantiate (enemyTracker) as GameObject);
				((GameObject)enemyList[i]).transform.parent = gameObject.transform;
			}
			((GameObject)enemyList[i]).transform.localPosition = center + enemies[i].transform.position;
		}
	}
}
