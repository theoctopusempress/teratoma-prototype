using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void SavePlayer (string saveName, PlayerController player, GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
        
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player,gameManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        Debug.Log("i told ya, loading");
        string path = Application.persistentDataPath + "/saves/" + "autoSave" + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);//new FileStream(path, FileMode.Open);

            try
            {


                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load file at {0}", path);
                stream.Close();
                return null;
            }
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
