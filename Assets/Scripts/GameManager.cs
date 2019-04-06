using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public AudioClip respawnSound;
	public AudioClip powerUpSound;
	public AudioClip energyUpSound;
	public AudioClip lifeUpSound;
	public AudioClip collectableSound;
	public AudioClip checkpointSound;
	private AudioSource soundEffects;
	public Text livestxt;
	public Text collectabletxt;
	public RectTransform barSize; //life bar
	public Text pausetxt;
	public bool paused = false;
	public Player player;
	public GameObject secretExit;
	public GameObject checkpoint;
	private GameObject closedDoor;
	private int nextScene=2;
	public Vector2 respawn;
	public int collectable = 0;
	private List<GameObject> spawners = new List<GameObject> ();
	//Player Stats
	private int lives = 3;
	public float energy = 1;
	public bool powerUp = false;

	private void Start () {
		soundEffects = GetComponent<AudioSource> ();
		player = player.GetComponent<Player> ();
		livestxt.text = "x" + lives.ToString ();
		collectabletxt.text = collectable.ToString()+"/5";
		closedDoor= GameObject.FindGameObjectWithTag ("closedDoor");
		respawn = closedDoor.transform.position;
		getSpawners ();
		soundEffects.Play ();
		soundEffects.loop = true;
	}

	private void Update () {
		//Pause
		if(Input.GetKeyDown(KeyCode.Return)){
			if (Time.timeScale == 0) {
				Time.timeScale = 1;
				pausetxt.text = " ";
				paused = false;
			} else {
				Time.timeScale = 0;
				pausetxt.text = "PAUSE";
				paused = true;
			}
		}

		//Dying
		if (energy <= 0)
			dieAndRespawn ();		

		//Life Bar
		barSize.anchorMax = new Vector2(-energy, 0f);
	}

	void dieAndRespawn(){
		if (lives > 1) {
			player.transform.position = respawn;
			lives--;
			powerUp = false;
			energy = 1;
			livestxt.text = "x" + lives.ToString ();
			soundEffects.PlayOneShot (respawnSound);
			foreach (var spawner in spawners) {
				spawner.GetComponent<Spawner> ().kill ();
			}
		} else {
			SceneManager.LoadScene ("GameOver");
		}
	}
		
	//Being killed, getting hit, chekpoint, recovering life
	void touchDamage(float x){
		if (energy >= x) {
			energy -= x;
		} else {
			energy = 0;
		}
	}
	private void projectileDamage(float damage){
		if (energy >= damage) {
			energy -= damage;
		} else {
			energy = 0;
		}
	}

	public void collisionEnter(Collision2D other){
		if (other.gameObject.tag == "platform") {
			transform.parent = other.transform;
		}
	}

	public void collisionExit(Collision2D other){
		if(other.gameObject.tag =="platform")
			transform.parent = null;
	}

	public void triggerStay(Collider2D other){
		if (other.gameObject.tag == "enemy1") {
			touchDamage (0.007f);
		} else if (other.gameObject.tag == "boss") {
			touchDamage (0.015f);
		} 
	}

	public void triggerEnter(Collider2D other){
		//EnergyUp
		if (other.tag == "energyUp") {
			soundEffects.PlayOneShot (energyUpSound);
			if (energy <= 0.5)
				energy = energy + 0.5f;
			else
				energy = 1;
			Destroy (other.gameObject);
		}

		//Instant death
		else if (other.tag == "kill")
			//dieAndRespawn ();
			energy=0;

		//Damage
		else if (other.tag == "cannonball") {
			projectileDamage (0.2f);
			Destroy (other.gameObject);
		}
		
		//Checkpoints
		else if (other.tag == "checkpoint") {
			soundEffects.PlayOneShot (checkpointSound);
			respawn = other.transform.position;
			Destroy (other.gameObject);
		}

		//Bullets
		else if (other.tag == "enemybullet") {
			projectileDamage (0.15f);
			Destroy (other.gameObject);
		} else if (other.tag == "bossBullet") {
			projectileDamage (0.3f);
			Destroy (other.gameObject);
		}

		//Changing scenes
		else if (other.tag == "levelEnd") {
			SceneManager.LoadScene ("Thx");
		} 

		else if (other.tag == "secretExitEnd") {
			// nextScene++;
			// SceneManager.LoadScene (nextScene);
			// DontDestroyOnLoad (this.gameObject);
			// player.transform.position = new Vector2 (0, 0);
			// nextScene++;
			// getSpawners ();
			// nextScene++;
			SceneManager.LoadScene ("Thx");
		}

		else if (other.tag == "showPath")
			secretExit.transform.position = new Vector3(0,0,-5f);

		//Items
		else if (other.tag == "powerUp") {
			soundEffects.PlayOneShot(powerUpSound);
			powerUp = true;
		}

		else if (other.tag == "lifeUp") {
			soundEffects.PlayOneShot (lifeUpSound);
			lives++;
			livestxt.text = "x" + lives.ToString ();
			Destroy (other.gameObject);
		}
	}

	//Function to add all spawners to a list
	private void  getSpawners(){
		foreach(var spawner in GameObject.FindGameObjectsWithTag("spawner")){
			spawners.Add (spawner);
		}
	}

	public void collectableFunction(int x){
		if (x == 0) {
			collectable++;
			collectabletxt.text = collectable.ToString () + "/5";
			soundEffects.PlayOneShot (collectableSound);
		} else if (x==1){
			collectable--;
			collectabletxt.text = collectable.ToString () + "/5";
		}
	}
}