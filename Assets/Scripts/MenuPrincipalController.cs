using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuPrincipalController : MonoBehaviour
{
    public Save save;
    private SaveManager saveManager = new SaveManager();
    public GameObject menuPrincipal;
    public GameObject menuConfig;
    public GameObject musica;

    void Start()
    {
        save = new Save();
        Save oldSave = saveManager.LoadGame();
        if(oldSave == null){
            (transform.GetComponentsInChildren<Button>()[1]).interactable = false;
            save.level = 0;
            save.soundSFX = 0.5f;
            save.music = 0.5f;
            saveManager.SaveGame(save);
        }
        else{
            save.level = oldSave.level;
            save.soundSFX = oldSave.soundSFX;
            save.music = oldSave.music;
        }

        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        for (int i = 0; i < sliders.Length; i++)
        {
            if(i == 0){
                sliders[i].value = save.soundSFX;
                Debug.Log(save.soundSFX);
            }
            else if(i == 1)
                sliders[i].value = save.music;
        }
    }

    public void Update(){
        if(Input.GetKey(KeyCode.Escape)){
            if(menuConfig.active){
                menuConfig.SetActive(false);
                menuPrincipal.SetActive(true);
            }
        }
    }

    public void Jogar(){
        // Saci
        save.level = 1;
        if(saveManager.SaveGame(save))
            SceneManager.LoadScene(1);
    }

    public void CarregarJogo(){
        SceneManager.LoadScene(save.level);
    }

    public void SalvarConfigs(){
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        for (int i = 0; i < sliders.Length; i++)
        {
            if(i == 0){
                save.soundSFX = sliders[i].value;
            }
            else if(i == 1)
                save.music = sliders[i].value;
        }

        saveManager.SaveGame(save);

        musica.GetComponent<AudioSource>().volume = save.music/100;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Saiu");
    }
}