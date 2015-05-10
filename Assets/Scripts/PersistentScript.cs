using UnityEngine;
using System.Collections;

public class PersistentScript : MonoBehaviour {

	public int score;
	public int wave;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
}
