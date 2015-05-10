using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour {
	public Interface iface;
	public GameObject Explosion;

	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.tag == "enemy") {
			iface.OnEnemyKill(0, false);
			GameObject explosion = Instantiate(Explosion, hit.gameObject.transform.position, Quaternion.identity) as GameObject;
			Destroy (explosion, 5.0f);
			Destroy(hit.gameObject);
			iface.OnPlayerDeath();
		}
	}
}
