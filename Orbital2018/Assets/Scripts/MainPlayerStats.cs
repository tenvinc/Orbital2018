using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainPlayerStats : MonoBehaviour {

    string filename = "PlayerStats.json";
    public static string path;

    public static SaveData saveData;

    public static MainPlayerStats instance;

    public static int TotalMonstersKilled;
    public static int TotalMonstersKilledStage;

    // public TextAsset jsonFile;

    public bool[] Levels;

    // NEW
    public static List<int> Attempts = new List<int>();
    public static List<int> monstersKilled = new List<int>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        path = System.IO.Path.Combine(Application.streamingAssetsPath, filename);
    }

    private void Start()
    {
        TotalMonstersKilled = 0;
        TotalMonstersKilledStage = 0;
    }

    void EditData(int monstersKilled,int Attempts,int index)
    {
        saveData.Stage[index].monstersKilled = monstersKilled;
        saveData.Stage[index].Attempts = Attempts;
    }

    void AddData(int monstersKilled,int Attempts)
    {
        StageData sd1 = new StageData();
        sd1.Attempts = Attempts;
        sd1.monstersKilled = monstersKilled;
        saveData.Stage.Add(sd1);
    }

    public void UpdateData()
    {
        if (MainPlayerStats.Attempts.Count < LevelManager.level.currIndex + 1) // CHECKED
        {
            MainPlayerStats.instance.AddData(PlayerStats.monstersKilled, PlayerStats.Attempt);
            MainPlayerStats.Attempts.Add(PlayerStats.Attempt);
            MainPlayerStats.monstersKilled.Add(PlayerStats.monstersKilled);
        }
        else
        {
            if (PlayerStats.monstersKilled > MainPlayerStats.monstersKilled[LevelManager.level.currIndex]) // CHECKED
            {
                MainPlayerStats.instance.EditData(PlayerStats.monstersKilled, PlayerStats.Attempt, LevelManager.level.currIndex);
            }
            else // CHECKED
            {
                MainPlayerStats.instance.EditData(MainPlayerStats.monstersKilled[LevelManager.level.currIndex], PlayerStats.Attempt, LevelManager.level.currIndex);
            }
        }
    }

    public void SaveData()
    {
        JsonWrapper wrapper = new JsonWrapper();
        wrapper.saveData = saveData;
        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(path, contents);
    }
    // Populates the mainplayerstats with the save data
    public void ReadData()
    {
        if (System.IO.File.Exists(path))
        {
            string contents = System.IO.File.ReadAllText(path);
            JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(contents);
            saveData = wrapper.saveData;
            // Continuous Read Data...
            if (Attempts.Count > 0 && monstersKilled.Count > 0)
            {
                // if stages in this current playthrough is less than the stages recorded in save data, add dummy values
                /*if (Attempts.Count < saveData.Stage.Count && monstersKilled.Count < saveData.Stage.Count)
                {
                    Attempts.Add(0);
                    monstersKilled.Add(0);
                }*/
                for (int i = 0; i < saveData.Stage.Count; i++)
                {
                    Attempts[i] = saveData.Stage[i].Attempts;
                    monstersKilled[i] = saveData.Stage[i].monstersKilled;
                }
            }

            // When you first start the game...
            else
            {
                for (int i = 0; i < saveData.Stage.Count; i++)
                {
                    Attempts.Add(saveData.Stage[i].Attempts);
                    monstersKilled.Add(saveData.Stage[i].monstersKilled);
                }
            }


        }
        else
        {
            Debug.Log("Unable to read the saved data, file does not exist");
            saveData = new SaveData();
        }

    }


}
