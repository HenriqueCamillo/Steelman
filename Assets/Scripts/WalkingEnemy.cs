using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour {
	private AudioSource soundEffects;
	public AudioClip explosion;
	public AudioClip shootSound;
	public Animator enemyAnim;
	private Rigidbody2D rigid;
	private Collider2D collid;
	public GameObject player;
	public EnemyBullet bullet;
	private Vector2 vel; //current enemy speed
	public float resistance = 15; //enemy's life
	private bool shooting = false;
	private bool attacking = false;
	private bool isTurnedRight = false;
	private bool exploding=false;

	private void Start () {
		rigid = GetComponent<Rigidbody2D>();
		collid = GetComponent<Collider2D> ();
		vel = rigid.velocity;
		player = GameObject.FindGameObjectWithTag ("Player");
		soundEffects = GetComponent<AudioSource> ();
	}
		
	private void Update () {
		if(attacking==false && exploding==false)
			StartCoroutine (attack ());
		
		if (shooting == false && exploding==false) 
			StartCoroutine (shoot ());		
		
		//Check player positon to choose coroutine and animation
		if (rigid.velocity.x > 0) {
			isTurnedRight = true;
			enemyAnim.SetBool ("isTurnedRight", true);
		} else if (rigid.velocity.x < 0) {
			isTurnedRight = false;
			enemyAnim.SetBool ("isTurnedRight",false);
		}
		
		if(rigid.velocity.x!=0)
			enemyAnim.SetBool ("isRunning",true);
		else
			enemyAnim.SetBool ("isRunning",false);
		
		//Destroy this object when its life gets to 0
		if (resistance <= 0 && exploding == false)
			StartCoroutine (explode ());
	}
	
	//explosion coroutine
	private IEnumerator explode(){
		enemyAnim.SetBool ("exploding", true);
		exploding = true;
		collid.enabled = false;
		soundEffects.PlayOneShot (explosion);
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
	
	//coroutine to keep shooting
	private IEnumerator shoot(){
		shooting = true;
		if (isTurnedRight==true)
			instantiateBullets (0.5f,0.2f,180);
		else
			instantiateBullets (-0.5f,0.2f,0);
		yield return new WaitForSeconds (0.7f);
		shooting = false;
	}
	/* This enemy keeps pursuing the player and shooting
	 * Once it need to change its direction, it gets a litte confused and has a delay*/
	private IEnumerator attack(){
		attacking = true;
		if (this.transform.position.x - player.transform.position.x > 1) {
			if (vel != rigid.velocity) {
				rigid.velocity = new Vector2 (0f, rigid.velocity.y);
				yield return new WaitForSeconds (0.7f);
			}
			rigid.velocity = new Vector2 (-10f, rigid.velocity.y);
			vel = rigid.velocity;

		} else if (this.transform.position.x - player.transform.position.x < -1) {
			if (vel != rigid.velocity) {
				rigid.velocity = new Vector2 (0f, rigid.velocity.y);
				yield return new WaitForSeconds (0.7f);
			}
			rigid.velocity = new Vector2 (10f, rigid.velocity.y);
			vel = rigid.velocity;
		} else {
			rigid.velocity = new Vector2 (0f, rigid.velocity.y);
			yield return new WaitForSeconds (0.7f);
		}
		attacking = false;
	}
		
	//Function to instantiate bullets
	private void instantiateBullets(float x, float y, int z){
		Instantiate (bullet, new Vector2 (this.transform.position.x + x, this.transform.position.y + y), Quaternion.Euler (0, 0, z));
		soundEffects.PlayOneShot (shootSound);
	}
}