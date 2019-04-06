using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
	private Rigidbody2D rigid;
	private Vector2 startPosition;

	private void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		startPosition = this.transform.position;
	}

	/* This platform will fall (or go up) once the player touches it
	 * After some time, it will go back to it's origin place*/
	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player")
			StartCoroutine (timeBeforeFall ());
	}

	private IEnumerator timeBeforeFall (){
		yield return new WaitForSeconds (0.1f);
		rigid.constraints = ~RigidbodyConstraints2D.FreezePositionY; //Freeze all constraints, unless Y
		yield return new WaitForSeconds (4f);
		rigid.constraints |= RigidbodyConstraints2D.FreezePositionY;
		this.transform.position = startPosition;
	}
}