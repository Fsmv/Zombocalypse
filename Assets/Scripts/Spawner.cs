using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject ObjectToSpawn;
	public int totalToSpawn;
	public float spawnInterval;
	public float variance;
	public Transform north, south, east, west;
	public float yPos;
	public Interface iface;
	public const int buffer = 5;
	
	void Start () {
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		for (int i = 0; i < totalToSpawn; i++) {
			yield return new WaitForSeconds(spawnInterval + Random.Range(-variance, variance));

			Vector3 position = new Vector3 (Random.Range(west.position.x + buffer, east.position.x - buffer),
			                                yPos,
			                                Random.Range(south.position.z + buffer, north.position.z - buffer));
			GameObject enemy = Instantiate (ObjectToSpawn, position, Quaternion.identity) as GameObject;
			enemy.transform.Rotate(0.0f, Random.Range(-180.0f, 180.0f), 0.0f);
			iface.OnEnemySpawn();
		}
	}
}