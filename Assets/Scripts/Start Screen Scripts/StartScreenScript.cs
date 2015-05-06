using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {
	
	public Texture background;
	private bool showMenu=false;
	
	void OnGUI () {
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height) , background);
		
		if (showMenu == true) {
			GUI.BeginGroup(new Rect(Screen.width/2-50,Screen.height/2 -45,100,90));
			
			GUI.Box(new Rect(0,0,100,90), "Credits");

			GUI.EndGroup();
		}
		
		if (GUI.Button (new Rect ((Screen.width*2) / 3 - 90, Screen.height / 2 + 175, 180, 65), "Credits")) {
			showMenu=!showMenu;
			Application.OpenURL("http://derekaudette.ottawaarts.com/music.php");
		}
		
		if(GUI.Button( new Rect( Screen.width/3 - 90,Screen.height/2 + 175, 180, 65),"Start the Game")) {
			Application.LoadLevel(1);
		}
	} // end OnGUI
}