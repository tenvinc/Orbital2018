using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StageComplete : MonoBehaviour
{

    public Text roundsText;
    // public int waveIndex;
    public AudioMixerSnapshot bgmComplete;

    void OnEnable()
    {
        roundsText.text = PlayerStats.monstersKilled.ToString() + "/" + PlayerStats.monstersKilledStage.ToString();
        bgmComplete.TransitionTo(0.05f);
    }

    public void Retry()
    {
        // Debug.Log("Retry");        
        MainPlayerStats.TotalMonstersKilled -= PlayerStats.monstersKilled;
        MainPlayerStats.TotalMonstersKilledStage -= PlayerStats.monstersKilledStage;
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextStage()
    {
        Debug.Log("Go to Next Stage");
        // SceneManager.LoadScene(0);
        LevelChanger.instance.FadeToNextLevel();
    }

    public void Menu()
    {
        Debug.Log("Go to menu");
        SceneManager.LoadScene(0); // WIP Hard Code for now
    }

}