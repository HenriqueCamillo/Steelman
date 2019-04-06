using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	private AudioSource musics;
	[SerializeField] GameObject credits;
	[SerializeField] GameObject howToPlay;


	private void Start(){
		musics = GetComponent<AudioSource> ();
		musics.Play ();
		musics.loop = true;

		credits.SetActive(false);
		howToPlay.SetActive(false);
	}

	//Function to start level 1
	public void PlayGame(){
		SceneManager.LoadScene (1);
	}

	//Funtion to load tutorial level
	public void TutorialLevel(){
		SceneManager.LoadScene ("Tutorial Scene");
	}

	//Function to display credits
	public void Credits(){
		credits.SetActive(true);
	}

	//Function to quit game
	public void QuitGame(){
		Application.Quit ();
	}
	//Function to go back to Main Menu
	public void TitleScreen(){
		SceneManager.LoadScene("MainMenu");
	}

	public void HowToPLay(){
		howToPlay.SetActive(true);
	}


	public void BackToMenu(){
		credits.SetActive(false);
		howToPlay.SetActive(false);
	}
}