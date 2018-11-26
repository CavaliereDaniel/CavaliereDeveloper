using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour {
	public float speed;
	public float attackDistance;
	public Transform[] wayPoints;

	protected int indexPoint = 0;
	protected Animator anim;
	protected bool facingRight = true;
	protected Transform target;
	protected float targetDistance;
	protected float targetInteractable;
	protected Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		targetDistance = transform.position.x - target.position.x;

		Move ();
	}

	protected void Flip(){
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	protected void Move(){
		transform.position = Vector2.MoveTowards (transform.position, wayPoints [indexPoint].transform.position, speed * Time.deltaTime);

		if (transform.position == wayPoints [indexPoint].transform.position) {
			indexPoint += 1;
		}

		if (indexPoint == wayPoints.Length) {
			indexPoint = 0;
		}
	}
}
