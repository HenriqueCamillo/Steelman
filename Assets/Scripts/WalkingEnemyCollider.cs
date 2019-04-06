using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyCollider : MonoBehaviour {
	//With this collider, the enemy won't fall, even though its BoxColliider2D is a trigger
	public WalkingEnemy wkEn;

	//Function to take damage and detroy bullets that hits it
	private void OnTriggerEnter2D(Collider2D other){

		//damage depends on the type of the bullet
		if (other.gameObject.tag == "bullet") {
			Destroy (other.gameObject);
			wkEn.resistance--;
		} else if (other.gameObject.tag == "upgradedBullet") {
			Destroy (other.gameObject);
			wkEn.resistance -= 2;
		} else if (other.gameObject.tag == "kill")
			Destroy (this.gameObject);
	}
}