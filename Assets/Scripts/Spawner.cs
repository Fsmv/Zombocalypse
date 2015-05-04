using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject ObjectToSpawn;
	public int totalToSpawn = 10;
	public float spawnInterval = 1.0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (spawn ());
	}

	private Vector3 getRandomPosition(){
		int random = Random.Range (0, 3);
		//make a random position on each side of the house to spawn the zombies
		switch(random){
		case 0:
			return new Vector3(Random.Range(-25,25), 100.0f,Random.Range(12,15));
		case 1:
			return new Vector3(Random.Range(-25,25), 100.0f,Random.Range(-15,-13));
		case 2:
			return new Vector3(Random.Range(-25,25), 100.0f,Random.Range(-13,-25));
		default:
			return new Vector3(Random.Range(19,25), 100.0f,Random.Range(-25,25));
		}

	}

	IEnumerator spawn(){
		for(int i = 0; i < totalToSpawn; i++){
			GameObject instance;
			
			Vector3 position = getRandomPosition();
			instance = Instantiate(ObjectToSpawn,position, Quaternion.identity) as GameObject;
			yield return new WaitForSeconds(spawnInterval);
		}
	}
	
}
