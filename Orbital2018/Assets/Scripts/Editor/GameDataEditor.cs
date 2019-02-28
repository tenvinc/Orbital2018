using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow {

    public SaveData saveData;

    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

    [MenuItem ("Window/Game Data Editor")]
    static void Init()
    {
        GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
        window.Show();
    }

    private void OnGUI()
    {
        if (saveData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("saveData");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save data"))
            {
                SaveData();
            }
        }
        if (GUILayout.Button("Read data"))
        {
            ReadData();
        }
    }

    private void ReadData()
    {

        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists (filePath))
        {
            string contents = File.ReadAllText(filePath);
            JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(contents);
            saveData = wrapper.saveData;
            // Continuous Read Data...
            if (MainPlayerStats.Attempts.Count > 0 && MainPlayerStats.monstersKilled.Count > 0)
            {
                for (int i = 0; i < saveData.Stage.Count; i++)
                {
                    MainPlayerStats.Attempts[i] = saveData.Stage[i].Attempts;
                    MainPlayerStats.monstersKilled[i] = saveData.Stage[i].monstersKilled;
                }
            }

            // When you starts the game...
            else
            {
                for (int i = 0; i < saveData.Stage.Count; i++)
                {
                    MainPlayerStats.Attempts.Add(saveData.Stage[i].Attempts);
                    MainPlayerStats.monstersKilled.Add(saveData.Stage[i].monstersKilled);
                }
            }
        }
        else
        {
            Debug.Log("Unable to read the saved data, file does not exist");
            saveData = new SaveData();
        }
    }

    private void SaveData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        JsonWrapper wrapper = new JsonWrapper();
        wrapper.saveData = saveData;
        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(filePath, contents);
    }


}
