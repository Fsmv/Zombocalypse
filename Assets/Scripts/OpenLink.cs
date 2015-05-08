using UnityEngine;
using System.Collections;

public class OpenLink : MonoBehaviour {
	public void go(string url) {
		Application.OpenURL(url);
	}
}
