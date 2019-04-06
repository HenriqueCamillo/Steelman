using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {
	private Rigidbody2D rigid;
	private float startPosX;
	private float startPosY;
	private float cannonballspped = -300 ;

	private void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (this.transform.right * cannonballspped, ForceMode2D.Force); //force to generate its movement

		//Get start position;
		startPosX = this.transform.position.x;
		startPosY = this.transform.position.y;
	}
	
	// Once it gets a little far from its start position, it will be destroyed
	private void Update () {
		if (Mathf.Abs(this.transform.position.x - startPosX) > 8f ||  Mathf.Abs(this.transform.position.y - startPosY) > 8f) {
			Destroy (this.gameObject);
		}				
	}

	// Cannonballs can be destroyed when it hits something
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "ground" || other.gameObject.tag == "door" || other.gameObject.tag == "platform") {
			Destroy (this.gameObject);
		}
		else if (other.gameObject.tag == "bullet") {
			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}
		//it won't destroy upgraded bullets
		else if (other.gameObject.tag == "upgradedBullet") {
			Destroy (this.gameObject);
		}
	}
}