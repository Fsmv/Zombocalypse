using UnityEngine;
using System.Collections;

public class BasicController : MonoBehaviour
{
	private Animator animator;
	private CharacterController controller;
	public float DirectionDampTime = .25f;
	private Vector3 deltaPosition;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	//private float jumpPos = 0.0f;
	private float verticalSpeed = 0;
	private float xVelocity = 0.0f;
	private float zVelocity = 0.0f;
	private Shooter shooter;
 
	void Start () {
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
		shooter = GetComponent<Shooter> ();
		if (animator.layerCount >= 2)
			animator.SetLayerWeight (1, 1);
	}
	
	void Update () {
		float accel = 1.0f;	
		if (controller.isGrounded) {	
			if (Input.GetKey (KeyCode.Space)) {
				animator.SetBool ("Jump", true);
				verticalSpeed = jumpSpeed;
			} else {
				animator.SetBool ("Jump", false);                
			}
			if (Input.GetKey (KeyCode.RightShift) || Input.GetKey (KeyCode.LeftShift)) {
				accel = 4.0f;	
			} else {
				accel = 1.0f;	
			}
		
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			
			animator.SetFloat ("Speed", (h * h + v * v) * accel, DirectionDampTime, Time.deltaTime);
			animator.SetFloat ("Direction", h, DirectionDampTime, Time.deltaTime);
			animator.SetFloat ("ZDirection", v, DirectionDampTime, Time.deltaTime);
		
			if (Input.GetKey (KeyCode.Q)) {
				animator.SetBool ("TurnLeft", true);
				transform.Rotate (Vector3.up * (Time.deltaTime * -45.0f), Space.World);
			} else {
				animator.SetBool ("TurnLeft", false);	
			}

			if (Input.GetKey (KeyCode.E)) {
				animator.SetBool ("TurnRight", true);
				transform.Rotate (Vector3.up * (Time.deltaTime * 45.0f), Space.World);	
			} else {
				animator.SetBool ("TurnRight", false);	
			}

			if (Input.GetKeyDown (KeyCode.F) && animator.layerCount >= 2) {
				animator.SetBool ("Grenade", true);
			} else {
				animator.SetBool ("Grenade", false);
			}

			if (shooter.isFiring () && animator.layerCount >= 2) {
				animator.SetBool ("Fire", true);
			} else if (animator.layerCount >= 2) {
				animator.SetBool ("Fire", false);
			}	
		}
	}
	
	void OnAnimatorMove () {
		Vector3 deltaPosition = animator.deltaPosition;
		
		if (controller.isGrounded) {
			xVelocity = animator.GetFloat ("Speed") * controller.velocity.x * 0.25f;
			zVelocity = animator.GetFloat ("Speed") * controller.velocity.z * 0.25f;
		}
		
		verticalSpeed += Physics.gravity.y * Time.deltaTime;	

		if (verticalSpeed <= 0) {
			animator.SetBool ("Jump", false);  
		}
				
		deltaPosition.y = verticalSpeed * Time.deltaTime;
				
		if (!controller.isGrounded) {
			deltaPosition.x = xVelocity * Time.deltaTime;
			deltaPosition.z = zVelocity * Time.deltaTime;
		}

		controller.Move (deltaPosition);
		if ((controller.collisionFlags & CollisionFlags.Below) != 0) {
			verticalSpeed = 0;	
		
		}
		transform.rotation = animator.rootRotation;
	}
}
