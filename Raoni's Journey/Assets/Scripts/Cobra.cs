using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour {
	[Header ("Variavel de Velocidade")]
	public float speed;
	[Space (10)]
	[Header ("Confirmaçã de Ataque")]
	public bool isAttack = false;
	[Space (10)]
	[Header ("Posicao dos Objetos do Caminho")]
	public Transform wayPointA;
	public bool isWayPointA;
	public Transform wayPointB;
	public bool isWayPointB;

	Animator anim;
	public static Cobra Instance;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (isAttack) {
			anim.SetTrigger ("Attack");
		} else {
			speed = -0.02f;
		}

		if (!isAttack) {
			transform.Translate (speed, 0, 0);
		} else {
			speed = 0;
		}

		if (transform.position.x >= wayPointA.transform.position.x) {
			isWayPointA = true;
			isWayPointB = false;
		}

		if (transform.position.x <= wayPointB.transform.position.x) {
			isWayPointA = false;
			isWayPointB = true;
		}

		if (isWayPointA) {
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		}

		if (isWayPointB) {
			transform.localRotation = Quaternion.Euler (0, 180, 0);

		}

	}

	public void Attack (){
		anim.SetTrigger("Attack");
		print ("Attack");
	}

}
