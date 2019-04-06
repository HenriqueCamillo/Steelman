using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlock : MonoBehaviour {
	private Renderer rend;
	private Collider2D collid;

	private void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		collid = GetComponent<Collider2D> ();
		collid.enabled = true;
	}

	/* This platform will be enabled or disabled by pressing J (the shoot button)
	 * It will start enabled by default*/
	private void Update () {
		if (Input.GetKeyDown (KeyCode.J) == true) {
			rend.enabled = !rend.enabled;
			collid.enabled = !collid.enabled;
		}
	}
}