using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Drown : MonoBehaviour {
	public float DrownTime = 15.0f;
	public float waterHeight = 60.0f;
	public Text breathText;
	public Interface iface;
	private float timeUnder;

	void Start() {
		timeUnder = 0;
	}
	
	void Update () {
		if (transform.position.y < waterHeight) {
			timeUnder += Time.deltaTime;
		} else {
			timeUnder = 0;
		}

		if (timeUnder > DrownTime) {
			iface.OnPlayerDeath();
			timeUnder = 0;
		}

		if (timeUnder == 0) {
			breathText.enabled = false;
		} else {
			breathText.enabled = true;
			breathText.text = new string('o', Mathf.CeilToInt(DrownTime - timeUnder));
		}
	}
}
