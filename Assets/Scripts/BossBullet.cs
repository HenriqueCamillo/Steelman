using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
	private Rigidbody2D rigid;
	private float startPosX;
	private float startPosY;
	private float enemyBulletSpeed = -500 ;

	private void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (this.transform.right * enemyBulletSpeed, ForceMode2D.Force); //force to generate its movement

		//Get start position
		startPosX = this.transform.position.x;
		startPosY = this.transform.position.y;
	}

	// Once it gets a little far from its start position, it will be destroyed
	void Update () {
		if (Mathf.Abs(this.transform.position.x - startPosX) > 8f ||  Mathf.Abs(this.transform.position.y - startPosY) > 8f) {
			Destroy (this.gameObject);
		}		
	}

	//Boss bullets will destroy all player bullets, and it will be destroyed just when it hits the ground
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "ground" || other.gameObject.tag == "door" || other.gameObject.tag == "platform") {
			Destroy (this.gameObject);
		}
		else if (other.gameObject.tag == "bullet" || other.gameObject.tag == "upgradedBullet") {
			Destroy (other.gameObject);
		}
	}
}