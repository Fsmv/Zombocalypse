using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour {
	public Interface iface;

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "enemy") {
			Destroy(hit.gameObject);
			iface.OnPlayerDeath();

		}
	}
}
