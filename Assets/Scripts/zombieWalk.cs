using UnityEngine;
using System.Collections;

public class zombieWalk : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		target.GetComponent<Animation>().Play("walk");
	}
}
