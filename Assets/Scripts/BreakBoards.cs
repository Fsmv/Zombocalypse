using UnityEngine;
using System.Collections;

public class BreakBoards : MonoBehaviour {
	public GameObject one,two,three;
	public float timeToBreak = 2.0f;
	public bool unbreakable = false;

	private AudioSource pounding, breaking;

	private int health = 3;
	private float lastTime = 0;
	private bool startedTimer = false;
	private int collisionCount = 0;
	private const int HEART_BEAT_START = 5;
	private int heartBeatTimer = HEART_BEAT_START;

	void Start() {
		foreach (AudioSource source in GetComponents<AudioSource>()) {
			if (source.clip.name == "pounding") {
				pounding = source;
			}else if(source.clip.name == "breaking") {
				breaking = source;
			}
		}
	}

	void Update() {
		// Horrific hack because onTriggerExit doesn't get called when destroy happens
		if (startedTimer) {
			if (heartBeatTimer <= 0) { 
				onLostContact ();
			} else {
				heartBeatTimer--;
			}
		}
	}

	void OnTriggerEnter(Collider collision) {
		collisionCount += 1;
		if (!startedTimer) {
			lastTime = Time.realtimeSinceStartup;
			startedTimer = true;
			pounding.Play ();
		}
	}

	void OnTriggerStay(Collider collision) {
		if (startedTimer && collision.gameObject.CompareTag ("enemy")) {
			heartBeatTimer = HEART_BEAT_START;
			if (Time.realtimeSinceStartup - lastTime >= timeToBreak) {
				if (!unbreakable) {
					health -= 1;
					breaking.Play ();
					switch (health) {
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
				}

				lastTime = Time.realtimeSinceStartup;
			}
		}
	}

	void onLostContact() {
		collisionCount -= 1;
		if (collisionCount == 0) {
			startedTimer = false;
			pounding.Stop();
		}
	}

	void OnTriggerExit(Collider collision) {
		onLostContact ();
	}
}
