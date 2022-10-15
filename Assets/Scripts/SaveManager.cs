using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager
{
    public Save LoadGame(){
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath;
        FileStream file;

        if(File.Exists(path + "/savegame.save")){
            file = File.Open(path + "/savegame.save", FileMode.Open);

            Save loads = (Save) bf.Deserialize(file);
            file.Close();

            return loads;
        }

        return null;
    }

    public bool SaveGame(Save s){
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath;
            FileStream file = File.Create(path + "/savegame.save");

            bf.Serialize(file, s);
            file.Close();
            
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}