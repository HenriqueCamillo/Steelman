using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {
	public Animator enemyAnim;
	private AudioSource soundEffects;
	public AudioClip shoot;
	public AudioClip explosion;
	private Collider2D collid;
	public GameObject player;
	public EnemyBullet enemyBullet;
	private bool canShoot = true;
	private int resistance = 10; //enemy's life
	private bool exploding = false;

	private void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		soundEffects = GetComponent<AudioSource> ();
		collid = GetComponent<Collider2D> ();
	}
	
	private void Update () {
		//Destroy this object when its life gets to 0
		if (resistance <= 0 && exploding==false)
			StartCoroutine (explode ());
		
		//It can shoot just if its X position is greater that player's X position
		if (this.transform.position.x > player.transform.position.x && canShoot == true && exploding ==false) {
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

	// This enemy will shoot 5 bullets and wait a litte, and then repeat
	private IEnumerator fire(){
		for (int i = 0; i < 5; i++) {
			instantiateEnemyBullet ();
			yield return new WaitForSeconds (0.1f);
		}
		yield return new WaitForSeconds (4f);
		canShoot = true;
	}

	//Function to instantiate bullets
	private void instantiateEnemyBullet(){
		Instantiate (enemyBullet, new Vector2 (this.transform.position.x -0.75f, this.transform.position.y + 0.15f), Quaternion.Euler (0, 0, 0));
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
			resistance -= 2;
		}
	}
}