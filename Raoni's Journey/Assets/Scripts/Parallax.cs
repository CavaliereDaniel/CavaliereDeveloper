using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public Transform target;
	public float paralaxSpeed;

	Vector3 previousPos;

	// Use this for initialization
	void Start () {
		previousPos = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate ((target.position.x - previousPos.x) / paralaxSpeed, 0, 0);
		previousPos = target.position;
	}
}
