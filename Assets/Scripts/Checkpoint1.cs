using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour {
	//check if player has already passed through this checkpoint
	public bool triggered = false; 

	private void OnTriggerEnter2D(Collider2D other){
		if (triggered == false) {
			if (other.gameObject.tag == "Player") {
				triggered = true;
			}
		}	
	}
}
