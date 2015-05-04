using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

	void OnGUI () {
		if(GUI.Button( new Rect( Screen.width/2 - 90,Screen.height/2 + 175, 180, 65),"Start the Game")) {
			Application.LoadLevel(1);
		}
	} // end OnGUI
}
