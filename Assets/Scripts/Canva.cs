using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canva : MonoBehaviour
{
    public bool isPaused = false;
	private GameObject menu;

    void Start(){
        menu = transform.GetChild(0).gameObject;
    }

	void Update(){
		if(Input.GetKeyUp(KeyCode.Escape)){
			if(!isPaused)
				PauseGame();

            else
            	ResumeGame();
		}
	}

	public void PauseGame()
    {
		isPaused = true;
		menu.SetActive(true);
		Time.timeScale = 0;
    }

	public void ResumeGame()
    {
		isPaused = false;
		menu.SetActive(false);
		Time.timeScale = 1;
    }

	public void Sair(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}