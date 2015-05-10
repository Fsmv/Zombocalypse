using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BreakBoards : MonoBehaviour {
	public GameObject one,two,three;
	public float timeToBreak = 2.0f;
	public float timeToRepair = 8.0f;
	public bool unbreakable = false;
	public Text repairingText;

	private AudioSource pounding, breaking, repairing;
	private NavMeshObstacle obstacle;

	private int health = 3;
	private float lastTime = 0;
	private bool startedTimer = false;
	private float lastRepairTime = 0;
	private bool startedRepair = false;
	private int collisionCount = 0;
	private const int HEART_BEAT_START = 5;
	private int heartBeatTimer = HEART_BEAT_START;

	void Start() {
		obstacle = GetComponent<NavMeshObstacle> ();
		foreach (AudioSource source in GetComponents<AudioSource>()) {
			if (source.clip.name == "pounding") {
				pounding = source;
			}else if(source.clip.name == "breaking") {
				breaking = source;
			}else if(source.clip.name == "repairing") {
				repairing = source;
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
		if (collision.gameObject.CompareTag ("enemy")) {
			collisionCount += 1;
			if (health > 0 && !startedTimer) {
				lastTime = Time.realtimeSinceStartup;
				startedTimer = true;
				pounding.Play ();
			}
		}
	}

	void OnTriggerStay(Collider collision) {
		if (health > 0 && startedTimer && collision.gameObject.CompareTag ("enemy")) {
			heartBeatTimer = HEART_BEAT_START;
			if (Time.realtimeSinceStartup - lastTime >= timeToBreak) {
				if (!unbreakable) {
					health -= 1;
					breaking.Play ();
					switch (health) {
					case 2:
						three.SetActive(false);
						break;
					case 1:
						two.SetActive(false);
						break;
					case 0:
						one.SetActive(false);
						obstacle.enabled = false;
						startedTimer = false;
						pounding.Stop();
						break;
					default:
						break;
					}
				}

				lastTime = Time.realtimeSinceStartup;
			}
		}else if (collision.gameObject.CompareTag ("Player")) {
			repairingText.enabled = true;

			if(Input.GetButton("Repair")) {
				if(!startedRepair) {
					lastRepairTime = Time.realtimeSinceStartup;
					startedRepair = true;
					repairing.Play();
				}

				int timeLeft = (int)(Time.realtimeSinceStartup - lastRepairTime);
				string progressText = "[";
				for (int i = 1; i <= (int) timeToRepair; i++) {
					if (i <= timeLeft) {
						progressText += "-";
					}else{
						progressText += " ";
					}
				}
				progressText += "]";
				repairingText.text = progressText;

				if (Time.realtimeSinceStartup - lastRepairTime >= timeToRepair) {
					if(health <= 3) {
					health += 1;
						switch (health) {
						case 3:
							three.SetActive(true);
							break;
						case 2:
							two.SetActive(true);
							break;
						case 1:
							one.SetActive(true);
							obstacle.enabled = true;
							break;
						default:
							break;
						}
					}
					lastRepairTime = Time.realtimeSinceStartup;
				}
			}else{
				repairingText.text = "Hold F to Repair";
				lastRepairTime = Time.realtimeSinceStartup;
				startedRepair = false;
				repairing.Stop();
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
		if (collision.gameObject.CompareTag ("enemy")) {
			onLostContact ();
		}else if(collision.gameObject.CompareTag("Player")) {
			startedRepair = false;
			repairingText.enabled = false;
			repairing.Stop();
		}
	}
}
