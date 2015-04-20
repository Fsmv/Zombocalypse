using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public GameObject MuzzleFlash;
	//public GameObject Tracer;
	//public float tracerOffset;
	public Vector3 FlashPosition;
	public Vector3 FlashRotation;
	public GameObject Gun;
	public GameObject Explosion, dust;
	public const int MaxMagazineSize = 10;
	private MouseAimFinal mouseAim;
	private bool firing;
	private int magazineLeft;

	private AudioSource shoot, empty, reload;

	public Interface iface;

	void Start () {
		mouseAim = GetComponent<MouseAimFinal> ();
		AudioSource[] sounds = Gun.GetComponents<AudioSource> ();
		foreach (AudioSource source in sounds) {
			if(source.clip.name == "shoot") {
				shoot = source;
			}else if(source.clip.name == "empty") {
				empty = source;
			}else if(source.clip.name == "reload") {
				reload = source;
			}
		}
		firing = false;

		magazineLeft = MaxMagazineSize;
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if(magazineLeft > 0) {
				GameObject hit = mouseAim.getCurrHit ().collider.gameObject;
				if (hit != null && hit.CompareTag ("enemy")) {
					EnemyData enemy = hit.GetComponent<EnemyData>();
					enemy.health -= 1;

					if(enemy.health <= 0) {
						iface.OnEnemyKill(enemy.scoreVal, true);
						Instantiate(Explosion, mouseAim.getCurrHit().point, Quaternion.identity);
						Destroy (hit);
					}
				}

				firing = true;
				GameObject flash = Instantiate(MuzzleFlash) as GameObject;
				flash.transform.parent = Gun.transform;
				flash.transform.localPosition = FlashPosition;
				flash.transform.localEulerAngles = FlashRotation;

				Instantiate (dust, mouseAim.getCurrHit().point, Quaternion.LookRotation(Camera.main.transform.position, Vector3.up));

				shoot.Play();
				magazineLeft -= 1;
			}else{
				empty.Play();
			}
		} else {
			firing = false;
		}

		if (Input.GetKeyDown ("r")) {
			magazineLeft = MaxMagazineSize;
			reload.Play();
		}
	}

	public int getMagazineLeft() {
		return magazineLeft;
	}

	public bool isFiring() {
		return firing;
	}
}
