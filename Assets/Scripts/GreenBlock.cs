using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlock : MonoBehaviour {
	private Renderer rend;
	private Collider2D collid;

	private void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = false;
		collid = GetComponent<Collider2D> ();
		collid.enabled = false;
	}

	/* This platform will be enabled or disabled by pressing J (the shoot button)
	 * It will start disabled by default*/
	private void Update () {
		if (Input.GetKeyDown (KeyCode.J) == true) {
			rend.enabled = !rend.enabled;
			collid.enabled = !collid.enabled;
		}
	}
}