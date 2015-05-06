using UnityEngine;
using System.Collections;

public class EndScreenScript : MonoBehaviour {
	
	public Texture background;
	
	void OnGUI () {
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height) , background);
		if(GUI.Button( new Rect( Screen.width/2 - 90,Screen.height/2 + 175, 180, 65),"Restart the Game")) {
			Application.LoadLevel(0);
		}
	} // end OnGUI
}