using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject enemyPrefab;
	private GameObject enemy;
	private GameObject player;
	private bool spawned=false; 

	private void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	//The spawner will spawn an enemy if the player position (x) is within a certain radius
	private void Update () {
		if (Mathf.Abs (player.transform.position.x - this.transform.position.x) < 20 && spawned == false) {
			spawned = true;
			enemy = Instantiate (enemyPrefab.gameObject, this.transform);
		} else if (Mathf.Abs (player.transform.position.x - this.transform.position.x) > 20 && spawned == true) {
			Destroy (enemy);
			spawned = false;
		} 
	}

	//Function to kill enemy (used when player dies)
	public void kill (){
		Destroy (enemy);
		spawned = false;
	}
}