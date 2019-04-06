using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesPlatform : MonoBehaviour {
	private Rigidbody2D rigid;
	private GameObject player;
	private Vector2 startPosition;
	private bool canStartCoroutine = true;

	private void Start () {
		startPosition = this.transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = GetComponent<Rigidbody2D> ();
	}

	private void Update () {
		if (canStartCoroutine == true && Mathf.Abs (this.transform.position.x - player.transform.position.x) < 3)
			StartCoroutine (fall ());
	}
	//This platform will fall when the player gets close to it
	private IEnumerator fall(){
		canStartCoroutine = false;
		rigid.constraints = ~RigidbodyConstraints2D.FreezePositionY; //freeze all constraints unless Y
		yield return new WaitForSeconds(2f);
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;
		this.transform.position = startPosition;
		canStartCoroutine = true;
	}
}