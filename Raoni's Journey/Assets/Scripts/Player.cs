using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[Header ("Atributos de Movimento do Player")]
	public float velo;
	public float hforce = 0;
	public float vforce = 0;
	[Space (10)]
	[Header ("Colisores das Formas do Player")]
	public GameObject mudaForma, raoniForm, oncaForm, gaviaoForm, cobraForm;
	[Space (10)]
	[Header ("Outros GameObjects")]
	public GameObject  panelFinish, item;
	[Space (10)]
	[Header ("Itens coletados do Player")]
	public List<GameObject> listItens; // Pre requisito: Colecoes.
	//public Sprite[] formas;
	[Space (10)]
	[Header ("Imagem do Player")]
	public SpriteRenderer render;
	[Space (10)]
	[Header ("Habilidades do Player")]
	public bool trueForm = true;
	public bool shiftForm = false;
	public bool quatroMove = false;
	[Space (10)]
	[Header ("Formas do Player")]
	public bool gaviao;
	public bool onca;
	public bool cobra;
	[Space (10)]
	[Header ("Variaveis de Pulo")]
	//variaveis do Pulo.
	public float jumpForce;
	public bool jump = false;

	public bool noChao = false;
	Transform groundCheck;
	public bool jumpActive = true;

	Rigidbody2D rb;
	Animator anim;
	[Space (10)]
	[Header ("Variaveis de Scala do Player")]
	public float xScale;
	public float yScale;
	[Space (10)]
	[Header ("Posicao de retorno do Player")]
	public Vector3 respawPlayer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		groundCheck = gameObject.transform.Find("GroundCheck");

	}

	// Update is called once per frame
	void Update () {

		hforce = Input.GetAxisRaw("Horizontal");
		anim.SetFloat("RaoniWalk", Mathf.Abs(hforce));
		vforce = Input.GetAxisRaw ("Vertical");
		//Girar Player:
		if(hforce>0)
		{
			render.flipX = false;
		}

		if(hforce<0)
		{
			render.flipX = true;
		}
		//Se habilidade de voo estiver ativa o player pode se mover em Y tb.
		if (!quatroMove) {
			rb.velocity = new Vector2 (hforce * velo, rb.velocity.y);
		} else {
			rb.velocity = new Vector2 (hforce * velo, vforce * velo);

		}

		//Animãcoes:

		/*
		 if (trueForm) {
			anim.SetTrigger ("HumanIddle");
		} else {
			trueForm = false;
		}
		*/

		if (gaviao) {
			//anim.SetFloat("FalconFly", Mathf.Abs(hforce));
			anim.SetBool ("isGaviao", true);
			anim.SetBool ("isRaoni", false); 
			//print("Gavião_Voando");
			xScale = 5;
			yScale = 5;
			transform.localScale = new Vector3 (xScale, yScale, 0);
		}

		if (onca) {
			anim.SetFloat("OncaWalk", Mathf.Abs(hforce));
			anim.SetBool ("isOnca", true); 
			anim.SetBool ("isRaoni", false); 
			//print("Onça_Andando");
			xScale = -8;
			yScale = 8;
			transform.localScale = new Vector3 (xScale, yScale, 0);

		} 

		if (cobra) {
			//anim.SetFloat("CobraWalk", Mathf.Abs(hforce));
			anim.SetBool ("isCobra", true); 
			anim.SetBool ("isRaoni", false); 
			//print("Cobra");
			xScale = -5;
			yScale = 5;
			transform.localScale = new Vector3 (xScale, yScale, 0);
		}


		MudaForma ();
		FormaOriginal ();
		JumpSkill ();
	}

	void MudaForma(){
		if (Input.GetButtonDown ("Jump") && trueForm && !shiftForm) {
			mudaForma.SetActive (true);
			trueForm = false;
		}
	}

	void OnTriggerEnter2D(Collider2D Other){

		if (Other.CompareTag ("Finish")) {
			transform.position = respawPlayer;
		}

		if (Other.CompareTag ("Item")) { // Pre requisito: Colecoes.
			listItens.Add (item);
			PassarFase ();
		}


		if(Other.gameObject.CompareTag("Gaviao") && !trueForm && !shiftForm){ //Fomas [1 Gavião:
			/*if (formas [0]) {
				render.sprite = formas [1];
			} else {
				render.sprite = formas [0];
			}
			*/

			mudaForma.SetActive (false);
			shiftForm = true;
			quatroMove = true;
			jumpActive = false;
			gaviao = true;
			raoniForm.SetActive (false);
			gaviaoForm.SetActive (true);
			oncaForm.SetActive (false);
			cobraForm.SetActive (false);
			SoundEffects.Instance.Gaviao (); // Pre requisito: Padrao de Projeto: Singleton.
			//SoundEffects.Instance.Boom ();
			//SoundEffects.Instance.Thunder ();
			anim.SetTrigger("ShiftForm");
		}

		if(Other.gameObject.CompareTag("Onca") && !trueForm && !shiftForm){ //Fomas [2] Onça:
			/*if (formas [0]) {
				render.sprite = formas [2];
			} else {
				render.sprite = formas [0];
			}
			anim.SetTrigger("OncaIddle");
			*/

			mudaForma.SetActive (false);
			shiftForm = true;
			onca = true;
			raoniForm.SetActive (false);
			gaviaoForm.SetActive (false);
			oncaForm.SetActive (true);
			cobraForm.SetActive (false);
			SoundEffects.Instance.Onca (); // Pre requisito: Padrao de Projeto: Singleton.
			anim.SetTrigger("ShiftForm");
		}

		if (Other.gameObject.CompareTag ("Cobra") && !trueForm && !shiftForm) { //Fomas [3] Cobra:
			/*if (formas [0]) {
				render.sprite = formas [3];
			} else {
				render.sprite = formas [0];
			}
			*/

		
			mudaForma.SetActive (false);
			shiftForm = true;
			cobra = true;
			jumpActive = false;
			raoniForm.SetActive (false);
			gaviaoForm.SetActive (false);
			oncaForm.SetActive (false);
			cobraForm.SetActive (true);
			SoundEffects.Instance.Cobra (); // Pre requisito: Padrao de Projeto: Singleton.
			anim.SetTrigger("ShiftForm");
		}

		if (Other.gameObject.CompareTag ("Attack") && !trueForm &&shiftForm && !onca) {
			trueForm = true;
			jumpActive = true;
			shiftForm = false;
			quatroMove = false;
			mudaForma.SetActive (false);
			//render.sprite = formas [0];
			xScale = 5;
			yScale = 5;
			transform.localScale = new Vector3 (xScale, yScale, 0);
			gaviao = false;
			onca = false;
			cobra = false;
			raoniForm.SetActive (true);
			gaviaoForm.SetActive (false);
			oncaForm.SetActive (false);
			cobraForm.SetActive (false);
			anim.SetBool ("isGaviao", false); 
			anim.SetBool ("isOnca", false); 
			anim.SetBool ("isCobra", false); 
			anim.SetBool ("isRaoni", true);
			Onca.Instance.Attack (); // Pre requisito: Padrao de Projeto: Singleton.
			//anim.SetTrigger("OncaAttack");
			//print ("Onça Atacou!");
		}

		if (Other.gameObject.CompareTag ("Attack2") && !trueForm &&shiftForm && !onca && !cobra) {
			trueForm = true;
			jumpActive = true;
			shiftForm = false;
			quatroMove = false;
			mudaForma.SetActive (false);
			//render.sprite = formas [0];
			xScale = 5;
			yScale = 5;
			transform.localScale = new Vector3 (xScale, yScale, 0);
			gaviao = false;
			onca = false;
			cobra = false;
			raoniForm.SetActive (true);
			gaviaoForm.SetActive (false);
			oncaForm.SetActive (false);
			cobraForm.SetActive (false);
			anim.SetBool ("isGaviao", false); 
			anim.SetBool ("isOnca", false); 
			anim.SetBool ("isCobra", false); 
			anim.SetBool ("isRaoni", true);
			Cobra.Instance.Attack (); // Pre requisito: Padrao de Projeto: Singleton.
			//anim.SetTrigger("CobraAttack");
			//print ("Onça Atacou!");
		}
	}

	void FormaOriginal(){
		if (Input.GetKeyDown ("e") && !trueForm && shiftForm) {
			trueForm = true;
			jumpActive = true;
			shiftForm = false;
			quatroMove = false;
			mudaForma.SetActive (false);
			//render.sprite = formas [0];
			xScale = 5;
			yScale = 5;
			transform.localScale = new Vector3 (xScale, yScale, 0);
			gaviao = false;
			onca = false;
			cobra = false;
			raoniForm.SetActive (true);
			gaviaoForm.SetActive (false);
			oncaForm.SetActive (false);
			cobraForm.SetActive (false);
			anim.SetBool ("isGaviao", false); 
			anim.SetBool ("isOnca", false); 
			anim.SetBool ("isCobra", false); 
			anim.SetBool ("isRaoni", true); 

		}
	}

	void JumpSkill(){
		//Pulo:
		noChao = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetKeyDown("w") && noChao && jumpActive)
		{
			jump = true;
			anim.SetTrigger("isJump");
		}

		if(jump)
		{
			rb.AddForce(new Vector2(0, jumpForce));
			jump = false;
		}

	}

	void PassarFase(){
		if (listItens.Count == 1) {
			panelFinish.SetActive (true);
		}
	}

}
