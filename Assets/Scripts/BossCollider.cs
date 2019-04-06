using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour {
	//With this collider, the boss won't fall, even though its BoxColliider2D is a trigger
	public Boss boss;

	//Function to take damage and detroy bullets that hits it
	private void OnTriggerEnter2D (Collider2D other){

		//damage depends on the type of the bullet
		if (other.tag == "bullet") {
			boss.resistance--;
			Destroy (other.gameObject);
		} else if (other.tag == "upgradedBullet") {
			boss.resistance -= 2;
			Destroy (other.gameObject);
		} else if (other.tag == "kill")
			Destroy (this.gameObject);
	}
}