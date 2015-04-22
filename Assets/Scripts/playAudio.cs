using UnityEngine;
using System.Collections;

public class playAudio : MonoBehaviour {
	//public AudioSource audio;
	void Start() {
		//audio.enabled = true;
		GetComponent<AudioSource>().Play ();
	}

}
