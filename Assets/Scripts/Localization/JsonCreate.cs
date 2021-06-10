using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class JsonCreate : MonoBehaviour
{
    private void Start()
    {
        MyData myObject = new MyData();
        myObject.menu_play = "Играть";
        myObject.weapon = "Автомат";    
        myObject.game_mode_capture_point = "Захват точки";

        string json = JsonUtility.ToJson(myObject);
        WriteToFile("gamedata.json", json);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }
    private string GetFilePath(string fileName)
    {
        Debug.Log(Application.persistentDataPath + "/" + fileName);
        return Application.persistentDataPath + "/" + fileName;
    }
}
