using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaviao : MonoBehaviour {
	[Header ("Variavel de Velocidade")]
	public float speed;
	[Space (10)]
	[Header ("Posicao dos Objetos do Caminho")]
	public Transform[] wayPoints;

	int indexPoint = 0;

	// Use this for initialization
	void Start () {

		transform.position = wayPoints [indexPoint].transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Move ();

	}

	void Move(){
		transform.position = Vector2.MoveTowards (transform.position, wayPoints [indexPoint].transform.position, speed * Time.deltaTime);

		if (transform.position == wayPoints [indexPoint].transform.position) {
			indexPoint += 1;
		}

		if (indexPoint == wayPoints.Length) {
			indexPoint = 0;
		}
	}


}
