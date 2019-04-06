using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1v : MonoBehaviour {
	public Animator enemyAnim;
	private AudioSource soundEffects;
	public AudioClip explosion;
	public AudioClip shoot;
	private Collider2D collid;
	public GameObject player;
	public Cannonball cannonball;
	private float resistance = 4; //enemy's life
	private bool canShoot = true;
	private bool exploding = false;

	private void Start () {
		collid = GetComponent<Collider2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		soundEffects = GetComponent<AudioSource> ();
	}
	
	private void Update () {
		//Destroy this object when its life gets to 0
		if (resistance <= 0 && exploding == false)
			StartCoroutine (explode ());
		
		//It can shoot just if it is higher than the player
		if (this.transform.position.y > player.transform.position.y && canShoot == true && exploding ==false) {
			canShoot = false;
			StartCoroutine (fire ());
		}
	}
	
	private IEnumerator explode(){
		enemyAnim.SetBool ("exploding", true);
		exploding = true;
		collid.enabled = false;
		soundEffects.PlayOneShot (explosion);
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
	
	/* This enemy will always be placed in a wall
	 * It can shoot in three directions (depending on the player position)
	 * It will shoot 3 bullets and wait a litte, and then, repeat */
	private IEnumerator fire(){
		for (int i = 0; i < 3; i++) {
			if (player.transform.position.x - this.transform.position.x > 2) {
				instantiateCannonball (135);
			} else if (player.transform.position.x - this.transform.position.x < -2) {
				instantiateCannonball (45);
			} else{
				instantiateCannonball (90);
			}
			yield return new WaitForSeconds (0.5f);
		}
		yield return new WaitForSeconds (2f);
		canShoot = true;
	}
		
	//Function to instantiate bullets
	private void instantiateCannonball(int z){
		Instantiate (cannonball, new Vector2 (this.transform.position.x -0.08f, this.transform.position.y - 0.75f), Quaternion.Euler (0, 0, z));
		soundEffects.PlayOneShot (shoot);
	}

	//Function to take damage and detroy bullets that hits it
	private void OnTriggerEnter2D(Collider2D other){

		//damage depends on the type of the bullet
		if (other.gameObject.tag == "bullet") {
			Destroy (other.gameObject);
			resistance--;
		} else if (other.gameObject.tag == "upgradedBullet") {
			Destroy (other.gameObject);
			resistance-=2;
		}
	}
}