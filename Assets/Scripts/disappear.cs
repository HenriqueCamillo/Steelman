using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappear : MonoBehaviour {
	private Renderer rend;
	private Collider2D collid;

	void Start () {
		StartCoroutine (timeGap ());
		rend = GetComponent<Renderer> ();
		collid = GetComponent<Collider2D> ();
		rend.enabled = false;
		collid.enabled = false;
	}

	//This platform will keep appearing and disappearing
	private IEnumerator timeGap(){
		while (true) {
			yield return new WaitForSeconds (1f);
			rend.enabled = !rend.enabled;
			collid.enabled = !collid.enabled;
		}
	}
}