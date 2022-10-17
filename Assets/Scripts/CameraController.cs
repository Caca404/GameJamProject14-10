using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	AudioSource audio;
    void Start(){
		Save save = new Save();
		SaveManager saveManager = new SaveManager();
        Save oldSave = saveManager.LoadGame();
        if(oldSave == null){
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

		audio = transform.GetChild(0).GetComponent<AudioSource>();
		audio.volume = save.music/100;
	}
}
