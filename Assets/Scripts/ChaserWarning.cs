using UnityEngine;
using System.Collections;

public class ChaserWarning : MonoBehaviour {
	public AudioSource warnBeep;
	private float frequency;

	void Start () {
		frequency = 0.0f;
		StartCoroutine ("WarnBeeping");
	}

	void Update () {
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("chaser");
		float closestDist = 999999;
		foreach (GameObject o in objects) {
			float dist = Vector3.Distance(o.transform.position, transform.position);
			if(dist < closestDist) {
				closestDist = dist;
			}
		}

		if (closestDist != 999999) {
			frequency = Mathf.Pow(1.3f, Mathf.Sqrt(closestDist/100.0f)) - 0.95f;
		} else {
			frequency = 0.0f;
		}
	}

	IEnumerator WarnBeeping() {
		while (true) {
			if(frequency != 0.0f) {
				warnBeep.Play();
				yield return new WaitForSeconds(frequency);
			} else {
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
