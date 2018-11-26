using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public GameObject musicPlayer, intro;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Inciar()
	{
		SceneManager.LoadScene("Main");	
		Destroy(musicPlayer.gameObject);

	}

	public void Voltar()
	{
		SceneManager.LoadScene("Menu");	
		Destroy(musicPlayer.gameObject);

	}

	public void Intro(){
		intro.SetActive (true);
	}
}
