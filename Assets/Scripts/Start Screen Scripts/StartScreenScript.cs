using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {
	
	void OnGUI () {
		Destroy (GameObject.Find("Persistent Data Storage"));
		if (GUI.Button (new Rect ((Screen.width*2) / 3 - 90, Screen.height / 2 + 175, 180, 65), "Credits")) {
			Application.LoadLevel(3);
		}
		
		if(GUI.Button( new Rect( Screen.width/3 - 90,Screen.height/2 + 175, 180, 65),"Start the Game")) {
			Application.LoadLevel(1);
		}
	} // end OnGUI
}