using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	public AudioClip explosion;
	public AudioClip shootSound;
	private AudioSource soundEffects;
	public Animator bossAnim;
	private Rigidbody2D rigid;
	private Collider2D collid;
	public BossBullet bossBullet;
	public GameObject player;
	private GameObject door;
	private Door doorScript;
	public int resistance = 70; //boss life
	private bool attacking = false;
	private bool shooting = false;
	private bool exploding = false;

	private void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		soundEffects = GetComponent<AudioSource> ();
		collid = GetComponent<Collider2D> ();
		door = GameObject.FindGameObjectWithTag ("door");
		doorScript = door.GetComponent<Door> ();
	}
	
	//The coroutine "attack" will be active all the time, but never two coroutines at the same time
	private void Update () {
		if(attacking==false && exploding == false)
			StartCoroutine (attack ());
		
		if (resistance <= 0 && exploding == false) {
			StartCoroutine (explode ());
			doorScript.openDoor ();
		}
		
		//Animation;
		if (rigid.velocity.x > 0)
			bossAnim.SetBool ("turnedRight", true);
		else if (rigid.velocity.x < 0)
			bossAnim.SetBool ("turnedRight", false);
		
		if (rigid.velocity.y != 0)
			bossAnim.SetBool ("jumping", true);
		else
			bossAnim.SetBool ("jumping", false);
		
	}

	private IEnumerator explode(){
		bossAnim.SetBool ("exploding", true);
		exploding = true;
		collid.enabled = false;
		soundEffects.PlayOneShot (explosion);
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}

	private IEnumerator attack(){
		attacking = true;

		/* The boss will use a melee attack while the player is near it
		 * It will jump, wait in the air a litte, go in the direction of the player, and then, repeat
		 * The jump direction depends on the player position*/
		if (Mathf.Abs (this.transform.position.x - player.transform.position.x) < 7) {
				if (player.transform.position.x - this.transform.position.x > 1)
					rigid.velocity = new Vector2 (5, 10);
				else if (player.transform.position.x - this.transform.position.x < -1)
					rigid.velocity = new Vector2 (-5, 10);
				else
					rigid.velocity = new Vector2 (0, 10);
		
				yield return new WaitForSeconds (0.5f);
				rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
				rigid.velocity = new Vector2 (0, 0);
				yield return new WaitForSeconds (0.25f);
				Vector2 playerAngle = new Vector2 ((player.transform.position.x - this.transform.position.x), (player.transform.position.y - this.transform.position.y));
				rigid.velocity = playerAngle.normalized * 7.5f;
				rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
				yield return new WaitForSeconds (0.75f);
				rigid.velocity = new Vector2 (0, 0);
				yield return new WaitForSeconds (1f);
		}
		/* If the player is far from the boss, the boss will start shooting in its direction,
		 * and walk towards it, until it gets close enough, and then keep just shooting*/
		else if(shooting ==false){
			shooting = true;
			if (Mathf.Abs (this.transform.position.x - player.transform.position.x) < 7.5f)
				rigid.velocity = new Vector2 (0, rigid.velocity.y);
			else if(player.transform.position.x > this.transform.position.x)
				rigid.velocity =new Vector2(1,rigid.velocity.y);
			else
				rigid.velocity =new Vector2(-1,rigid.velocity.y);
			if (player.transform.position.x > this.transform.position.x)
				shoot (1.5f, 0.15f, 180);
			else 
				shoot (-1.5f, 0.15f, 0);
			yield return new WaitForSeconds(1f);
			
			shooting=false;
		}
		attacking = false;
	}
		
	//Function to instantiate bullets
	private void shoot(float x, float y, int z){
		Instantiate (bossBullet, new Vector2 (this.transform.position.x + x, this.transform.position.y + y), Quaternion.Euler (0, 0, z));
		soundEffects.PlayOneShot (shootSound);
	}
}
