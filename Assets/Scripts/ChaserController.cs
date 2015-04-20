using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ChaserController : MonoBehaviour {
	CharacterController controller;
	public float ForwardSpeed = 10.0f;
	public float maxRotationSpeed = 5.0f;
	public GameObject Explosion;
	Vector3 directionVector;
	private Interface iface;
	private EnemyData myData;
	private Transform playerTransform;
	
	// Use this for initialization
	void Start () {
		myData = GetComponent<EnemyData> ();
		iface = GameObject.Find ("Game Manager").GetComponent<Interface> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		controller = GetComponent<CharacterController> ();
		directionVector = transform.forward;

		Physics.IgnoreCollision (controller.collider, GetComponent<CapsuleCollider> ());
	}
	
	// Update is called once per frame
	void Update () {
		directionVector = playerTransform.position - transform.position;
		transform.forward = Vector3.Slerp (transform.forward, directionVector, Time.deltaTime * maxRotationSpeed);

		Vector3 movement = transform.forward * (ForwardSpeed * Time.deltaTime);
		
		if (!controller.isGrounded) {
			movement += Physics.gravity;
		}

		controller.Move (movement);
		
		//if we fell through the world just delete the enemy
		if (transform.position.y < 0) {
			iface.OnEnemyKill(0, false);
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.CompareTag ("Player")) {
			iface.OnPlayerDeath();
			iface.OnEnemyKill(myData.scoreVal, true);
			Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}