using UnityEngine;
using System.Collections;

public class EndScreenScript : MonoBehaviour {
	
	public Texture background;
	public int wave=0;
	public int score=0; //The score box can accomodate a score with 8 digits such as 10000000
	
	void OnGUI () {
		GameObject data = GameObject.Find ("Persistent Data Storage");
		wave = data.GetComponent<PersistentScript> ().wave;
		score = data.GetComponent<PersistentScript> ().score;
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height) , background);

		GUI.BeginGroup(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 45, 150, 90));
		GUI.Box(new Rect(0,0,150,90), "Results");                //Everything is a box because you can
		GUI.Box(new Rect(30,30,80,20), "Wave: " + wave);		 //click in text areas and fields
		GUI.Box(new Rect (30, 60, 110, 20), "Score: " + score);
		GUI.EndGroup();

		if(GUI.Button( new Rect( Screen.width/2 - 90,Screen.height/2 + 175, 180, 65),"Restart the Game")) {
			Application.LoadLevel(0);
		}
	} // end OnGUI
}