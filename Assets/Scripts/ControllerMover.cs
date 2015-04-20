using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ControllerMover : MonoBehaviour {
	CharacterController controller;
	public float ForwardSpeed = 5.0f;
	public float maxRotationSpeed = 5.0f;
	Vector3 directionVector;
	private Interface iface;

	// Use this for initialization
	void Start () {
		iface = GameObject.Find ("Game Manager").GetComponent<Interface> ();
		controller = GetComponent<CharacterController> ();
		directionVector = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = directionVector * (ForwardSpeed * Time.deltaTime);

		if (!controller.isGrounded) {
			movement += Physics.gravity;
		}

		Vector3 lastPos = transform.position;
		controller.Move(movement);
		transform.forward = Vector3.Slerp (transform.forward, directionVector, Time.deltaTime * maxRotationSpeed);

		//if we're stuck or we fell through the world just delete the enemy
		if (lastPos == transform.position || transform.position.y < 0) {
			Destroy(gameObject);
			iface.OnEnemyKill(0, false);
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		directionVector = Vector3.Reflect (directionVector, hit.normal);
		directionVector.y = 0;
		directionVector.Normalize();
	}
}
