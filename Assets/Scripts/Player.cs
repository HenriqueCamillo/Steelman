using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
	public Bullet bullet;
	public Bullet upgradedBullet;
	private bool isFiring = false;
	private bool turnedRight=true;

	//Animation//Sound
	public GameManager gameManager;
	public Animator playerAnim;
	public AudioClip shoot;
	private AudioSource soundEffects;

	//Movements
	private float speed = 5;
	private float jump = 7;
	private Rigidbody2D rigid;

	//checking if player is grounded
	public Transform groundCheck, groundCheck2;
	public float groundCheckRadius;
	public LayerMask groundLayers;
	private bool isGrounded1, isGrounded2;
		
	private void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		soundEffects = GetComponent<AudioSource>();
	}

	//Check if it's grounded
	private void FixedUpdate(){
		isGrounded1 = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayers);
		isGrounded2 = Physics2D.OverlapCircle (groundCheck2.position, groundCheckRadius, groundLayers);
	}
		
	private void Update () {
		if (gameManager.paused == false) {
			//Check side/ Animation
			if (rigid.velocity.x > 0) {
				turnedRight = true;
				playerAnim.SetBool ("turnedRight", true);
			} else if (rigid.velocity.x < 0) {
				turnedRight = false;
				playerAnim.SetBool ("turnedRight", false);
			}

			if (rigid.velocity.x != 0)
				playerAnim.SetBool ("running", true);
			else
				playerAnim.SetBool ("running", false);

			if (Input.GetKey (KeyCode.W))
				playerAnim.SetBool ("AimingUp", true);
			else
				playerAnim.SetBool ("AimingUp", false);

			if (Input.GetKey (KeyCode.W) == false) {
				if (Input.GetKey (KeyCode.S))
					playerAnim.SetBool ("AimingDown", true);
				else
					playerAnim.SetBool ("AimingDown", false);
			}
				
			//Jump
			if ((isGrounded1 == true || isGrounded2 == true)) {
				if (Input.GetKeyDown (KeyCode.K))
					rigid.velocity = new Vector2 (rigid.velocity.x, jump);
				playerAnim.SetBool ("Jumping", false);
			} else if (isGrounded1 == false && isGrounded2 == false)
				playerAnim.SetBool ("Jumping", true);

			//Movement
			rigid.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, rigid.velocity.y);

			//Shooting directions
			if (isFiring == false) {
				if (Input.GetKeyDown (KeyCode.J) == true)
					StartCoroutine (canFire ());
			
				if (Input.GetKeyDown (KeyCode.J) == true && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.W) == true) {
					if (turnedRight == true) 				//condição para ele não poder andar para esquerda e atirar para direita
					instantiateBullet (0.7f, 1f, 45);
				} else if (Input.GetKeyDown (KeyCode.J) == true && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.S) == true) {
					if (turnedRight == true)
						instantiateBullet (0.7f, -0.5f, -45);
				} else if (Input.GetKeyDown (KeyCode.J) == true && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == true)
					instantiateBullet (-0.7f, -0.5f, 225);
				else if (Input.GetKeyDown (KeyCode.J) == true && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.W) == true)
					instantiateBullet (-0.7f, 1f, 135);
				else if (Input.GetKeyDown (KeyCode.J) == true && Input.GetKey (KeyCode.W) == true)
					instantiateBullet (0f, 1f, 90);
				else if (Input.GetKeyDown (KeyCode.J) == true && Input.GetKey (KeyCode.S) == true) {
					if (turnedRight == true)
						instantiateBullet (-0.4f, -0.7f, -90);
					else
						instantiateBullet (0.4f, -0.7f, -90);
				} else if (Input.GetKeyDown (KeyCode.J) == true) {
					if (turnedRight == true)
						instantiateBullet (0.7f, 0.3f, 0);
					else
						instantiateBullet (-0.7f, 0.3f, 180);				
				}
			}
		}
	}

	//Shoot Delay
	private IEnumerator canFire(){
		isFiring = true;
		yield return new WaitForSeconds (0.1f);
		isFiring = false;
	}

	//Function to instantiate bullets
	private void instantiateBullet(float x, float y, int z){
		if(gameManager.powerUp == false)
			Instantiate (bullet, new Vector2 (this.transform.position.x + x, this.transform.position.y + y), Quaternion.Euler (0, 0, z));
		else
			Instantiate (upgradedBullet, new Vector2 (this.transform.position.x + x, this.transform.position.y + y), Quaternion.Euler (0, 0, z));
		soundEffects.PlayOneShot (shoot);
	}

	//Function to detect collisions
	private void OnTriggerEnter2D(Collider2D other){
		gameManager.triggerEnter (other);
	}
		
	private void OnTriggerStay2D(Collider2D other){
		gameManager.triggerStay (other);
	}

	private void OnCollisionEnter2D(Collision2D other){
		gameManager.collisionEnter (other);
	}

	private void OnCollisionExit2D(Collision2D other){
		gameManager.collisionExit (other);
	}
}