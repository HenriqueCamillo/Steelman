using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpDown : MonoBehaviour {
	private Vector2 startPosition;
	private Vector2 actualPosition;
	public int sign;
	public float distance;

	private void Start () {
		startPosition = this.transform.position;
	}

	//This platform will keep moving up and down
	private void Update () {
		transform.position = startPosition + new Vector2 (0, -Mathf.Sin (Time.time)*sign*distance);
	}
}