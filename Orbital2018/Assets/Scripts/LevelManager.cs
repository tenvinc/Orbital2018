using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour {

    public static LevelManager level;
    public WaveSpawner spawner;
    public TagMasterSO tagmasterso;

    public static bool gameEnded;
    public GameObject gameOverUI;

    public static bool stageCompleted;

    public GameObject StageCompleteUI;
    public GameObject AttemptFont;
    public AudioMixer BGM;

    public int currIndex;

    private int totalCount;
    [HideInInspector]public int currCount = 0;

    void Awake()
    {
        if (level != null)
        {
            Debug.Log("More than 1 level manager in scene");
            Destroy(gameObject);
        }
        else
        {
            currIndex = SceneManager.GetActiveScene().buildIndex - 1;
            level = this;
        }
    }



    void Start()
    {
        MainPlayerStats.instance.ReadData(); // CHECKED
        //Debug.Log(currIndex);
       // Debug.Log(MainPlayerStats.Attempts.Count);
       // Debug.Log(MainPlayerStats.Attempts[currIndex]);
        BGM.SetFloat("MasterVolume",MainPlayerStats.saveData.Volume);
        if (MainPlayerStats.Attempts.Count >= currIndex + 1) // CHECKED
        {
            PlayerStats.Attempt = MainPlayerStats.Attempts[currIndex];
        }
        /*
        for (int i = 0; i < Waypoints.waypoints.Length - 1; i++)
        {
            GameObject line = ObjectPool.instance.GetPooledObject(1);
            if (line == null)
            {
                return;
            }
            line.GetComponent<Bezier>().point0 = Waypoints.waypoints[i];
            line.GetComponent<Bezier>().point1 = Waypoints.waypoints[i+1];
            line.SetActive(true);
        }
        */
        gameEnded = false;
        stageCompleted = false;
        totalCount = 0;
        currCount = 0;
        PlayerStats.monstersKilled = 0;
        for (int i=0; i<spawner.WM.wave.Length; i++)
        {
            totalCount += spawner.WM.wave[i].spawnInstructions.Length;
        }
    }

    void Update()
    {
        // Debug.Log(totalCount);
        if (gameEnded || stageCompleted)
            return;
        if (PlayerStats.Lives <= 0)
        {
            //Debug.Log("You died");
            Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);
            MainPlayerStats.instance.UpdateData();

            Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);
            // Save to json
            MainPlayerStats.instance.SaveData(); // CHECKED

            Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);
            EndGame();
        }
        else if ((currCount == totalCount) && (currCount != 0 && totalCount != 0))
        {
            // LevelChanger.instance.FadeToNextLevel();

            stageCompleted = true;

            PlayerStats.monstersKilledStage = totalCount;
            MainPlayerStats.TotalMonstersKilled += PlayerStats.monstersKilled;
            MainPlayerStats.TotalMonstersKilledStage += PlayerStats.monstersKilledStage;

            // MainPlayerStats.instance.Levels[SceneManager.GetActiveScene().buildIndex] = true; // WIP

            MainPlayerStats.instance.UpdateData();
            Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);


            // Setting levelReached to be a higher no.
            // Never been here before
            if (SceneManager.GetActiveScene().buildIndex == MainPlayerStats.saveData.levelReached) // CHECKED
            {
                MainPlayerStats.saveData.levelReached = SceneManager.GetActiveScene().buildIndex + 1;
            }

            MainPlayerStats.instance.SaveData(); // CHECKED
            Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);

            StageCompleteUI.SetActive(true);

            return;
        }
    }

    void EndGame()
    {
        gameEnded = true;

        // gameOverUI.GetComponent<GameOver>().waveIndex = spawner.GetWaveIndex() - 1;

        PlayerStats.monstersKilledStage = totalCount;

        gameOverUI.SetActive(true);
    }

    public void SoftResetLevel()
    {
        spawner.ResetWaveNum(); // Reset Wave Num to 0
        List<GameObject> enemiesInScene = new List<GameObject>();
        // Destroying enemies currently in the scene
        for (int i = 0; i < tagmasterso.Tags.Count; i++)
        {
            string currTag = tagmasterso.Tags[i];
            GameObject[] temp = GameObject.FindGameObjectsWithTag(currTag);
            for (int j = 0; j < temp.Length; j++)
            {
                enemiesInScene.Add(temp[j]);
            }
        }
        for (int i=0; i< enemiesInScene.Count; i++)
        {
            Destroy(enemiesInScene[i]);
        }
        // Resetting debuff status of every tower
        for (int i=0; i< tagmasterso.towerTags.Count; i++)
        {
            string currTag = tagmasterso.towerTags[i];
            GameObject[] temp = GameObject.FindGameObjectsWithTag(currTag);
            for (int j = 0; j < temp.Length; j++)
            {
                TurretShooting turret = temp[j].GetComponent<TurretShooting>();
                if (turret != null)
                {
                    turret.resetDebuff();
                    turret.ResetBlind();
                }
            }
        }
        // Reset gold
        PlayerStats.Money -= PlayerStats.moneyEarned;
        PlayerStats.moneyEarned = 0;
        // Reset health
        PlayerStats.Lives += PlayerStats.livesLost;
        PlayerStats.livesLost = 0;
        // Reset currCount
        currCount = 0;
        // Reset monstersKilled
        PlayerStats.monstersKilled = 0;
        // Reenable the ui
        InterfaceManager.ui.ResetStage();
        // Reset the game complete, game ended, game complete ui, game ended ui
        gameEnded = false;
        stageCompleted = false;
        gameOverUI.SetActive(false);
        StageCompleteUI.SetActive(false);
        // Reset the status of the pause button/ resume button
        InterfaceManager.ui.PauseText.SetActive(true);
        InterfaceManager.ui.ResumeText.SetActive(false);
        // Reset bezier lines
        /*
        for (int i = 0; i < Waypoints.waypoints.Length - 1; i++)
        {
            GameObject line = ObjectPool.instance.GetPooledObject(1);
            if (line == null)
            {
                return;
            }
            line.GetComponent<Bezier>().point0 = Waypoints.waypoints[i];
            line.GetComponent<Bezier>().point1 = Waypoints.waypoints[i + 1];
            line.SetActive(true);
        }
        */
        MainPlayerStats.instance.ReadData();
    }

    public void HardResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
