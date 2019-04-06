using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public Animator doorAnim;

	//This door will open (and be destroyed) when a boss is detroyed
	public void openDoor(){
		doorAnim.SetBool ("open", true);
		StartCoroutine (open ());
	}

	private IEnumerator open(){
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}