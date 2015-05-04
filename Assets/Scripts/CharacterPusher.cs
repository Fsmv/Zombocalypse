using UnityEngine;
using System.Collections;

public class CharacterPusher : MonoBehaviour {
	public float pushForce;

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody hitRigid = hit.gameObject.GetComponent<Rigidbody> ();
		if (hitRigid != null) {
			hitRigid.AddForce (hit.moveDirection * pushForce);
		}
	}
}
