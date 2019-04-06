using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	private GameManager gameManagerScript;
	private GameObject gameManager;
	private Renderer rend;
	private Collider2D collid;
	private bool pickedUp=false;

	private void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		rend = GetComponent<Renderer> ();
		collid = GetComponent<Collider2D> ();
		gameManagerScript = gameManager.GetComponent<GameManager> ();
	}

	private void Update () {
		if (gameManagerScript.respawn.x > this.transform.position.x)
			Destroy (this.gameObject);
		else if (gameManagerScript.energy <= 0 && pickedUp==true) {
			rend.enabled = true;
			collid.enabled = true;
			gameManagerScript.collectableFunction (1);
			pickedUp=false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			rend.enabled = false;
			collid.enabled = false;
			gameManagerScript.collectableFunction(0);
			pickedUp = true;
		}
	}
}