using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Rigidbody2D rigid;
	public float bulletspeed;
	private float startPosX;
	private float startPosY;

	private void Start () {
		rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce (this.transform.right * bulletspeed, ForceMode2D.Force); //force to generate its movement

		//Get start position
		startPosX = this.transform.position.x;
		startPosY = this.transform.position.y;
	}
	
	// Once the bullet gets a little far from its start position, it will be destroyed
	private void Update (){
		if (Mathf.Abs(this.transform.position.x - startPosX) > 8f ||  Mathf.Abs(this.transform.position.y - startPosY) > 8f) {
			Destroy (this.gameObject);
		}		
	}

	//It will be destroyed when it hits the ground
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "ground" || other.gameObject.tag == "secretExit" || other.gameObject.tag == "door" || other.gameObject.tag == "platform") {
			Destroy (this.gameObject);
		}
	}
}