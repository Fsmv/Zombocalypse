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
		playerTracker.layer = 5;
		enemyList = new ArrayList ();
	}

	void Update () {
		playerTracker.transform.localPosition = center + GameObject.FindGameObjectWithTag ("Player").transform.position;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		if (enemyList.Count > enemies.GetLength (0)) {
			ArrayList deleteList = new ArrayList();
			for (int i = enemies.GetLength (0) - 1; i < enemyList.Count; ++i) {
				deleteList.Add(enemyList[i]);
			}

			foreach (GameObject go in deleteList) {
				Destroy (go);
				enemyList.Remove(go);
			}
		}

		for (int i = 0; i < enemies.GetLength(0); ++i) {
			if(i >= enemyList.Count) {
				enemyList.Add (Instantiate (enemyTracker) as GameObject);
				((GameObject)enemyList[i]).transform.parent = gameObject.transform;
				((GameObject)enemyList[i]).layer = 5;
			}
			((GameObject)enemyList[i]).transform.localPosition = center + enemies[i].transform.position;
		}
	}
}
