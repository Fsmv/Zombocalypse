using UnityEngine;
using System.Collections;

public class BreakBoards : MonoBehaviour {
	public GameObject one,two,three;
	public float timeToBreak = 2.0f;
	private Collider collider;
	private int health = 3;
	private float lastTime = 0;
	private bool startedTimer = true;
	private int collisionCount = 0;

	void Start() {
		collider = GetComponent<Collider> ();
	}

	void OnTriggerEnter(Collider collision) {
		collisionCount += 1;
		if (!startedTimer) {
			lastTime = Time.realtimeSinceStartup;
			startedTimer = true;
			Debug.Log ("Started Timer");
		}
	}

	void OnTriggerStay(Collider collision) {
		if (startedTimer && Time.realtimeSinceStartup - lastTime >= timeToBreak &&
		    collision.gameObject.CompareTag ("enemy")) {
			health -= 1;
			Debug.Log ("Breaking board");
			switch(health) {
			case 2:
				Destroy (three);
				break;
			case 1:
				Destroy (two);
				break;
			case 0:
				Destroy (one);
				Destroy (gameObject);
				break;
			default:
				break;
			}

			lastTime = Time.realtimeSinceStartup;
		}
	}

	void OnTriggerExit(Collider collision) {
		collisionCount -= 1;
		if (collisionCount == 0) {
			startedTimer = false;
		}
	}
}
