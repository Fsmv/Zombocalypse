using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	void OnGUI () {		
		if(GUI.Button( new Rect( (Screen.width*2)/3 - 20,Screen.height/2, 180, 65),"Main Menu")) {
			Application.LoadLevel(0);
		}
	} // end OnGUI
}
