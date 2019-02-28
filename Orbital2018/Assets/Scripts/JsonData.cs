using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour {

    string filename = "data.json";
    string path;

    SaveData saveData = new SaveData();

	// Use this for initialization
	void Start () {
        path = Application.persistentDataPath + "/" + filename;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        SaveData();
    }

    void SaveData()
    {
        JsonWrapper wrapper = new JsonWrapper();
        wrapper.saveData = saveData;
        string contents = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(path, contents);
    }

    void ReadData()
    {
        try
        {

            if (System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
                JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(contents);
                saveData = wrapper.saveData;
            }
            else
            {
                Debug.Log("Unable to read the saved data, file does not exist");
                saveData = new SaveData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
