using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

	public AudioClip[] playList;
	public AudioSource audios;
	public GameObject MusicPlayer;

	public static SoundEffects Instance; // Pre requisito: Padrao de Projeto: Singleton.

	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (this);
		}
	}

	// Use this for initialization
	void Start () {
		audios = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Gaviao(){
		audios.clip = playList [0];
		audios.Play ();
	}

	public void Cobra(){
		audios.clip = playList [1];
		audios.Play ();
	}

	public void Onca(){
		audios.clip = playList [2];
		audios.Play ();
	}

	public void Boom(){
		audios.clip = playList [3];
		audios.Play ();
	}

	public void Thunder(){
		audios.clip = playList [4];
		audios.Play ();
	}
}
