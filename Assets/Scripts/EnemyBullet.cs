using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	private float startPosX;
	private float startPosY;
	private float enemyBulletSpeed = -700 ;
	private Rigidbody2D rigid;

	private void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (this.transform.right * enemyBulletSpeed, ForceMode2D.Force); //force to generate its movement

		//Get start position
		startPosX = this.transform.position.x;
		startPosY = this.transform.position.y;
	}

	// Once it gets a little far from its start position, it will be destroyed
	private void Update () {
		if (Mathf.Abs(this.transform.position.x - startPosX) > 12f ||  Mathf.Abs(this.transform.position.y - startPosY) > 12f) {
			Destroy (this.gameObject);
		}		
	}

	//It will be destroyed when it hits the ground
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "ground" || other.gameObject.tag == "door" || other.gameObject.tag == "platform") {
			Destroy (this.gameObject);
		}
		else if (other.gameObject.tag == "bullet") {
			Destroy (other.gameObject);
		}
		else if (other.gameObject.tag == "upgradedBullet") {
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
	}
}