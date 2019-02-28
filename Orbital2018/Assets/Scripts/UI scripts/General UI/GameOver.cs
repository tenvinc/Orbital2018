using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Text roundsText;
    // public int waveIndex;

    void OnEnable()
    {
        // roundsText.text = waveIndex.ToString();
        roundsText.text = PlayerStats.monstersKilled.ToString() + "/" + PlayerStats.monstersKilledStage.ToString();
    }

    public void Retry ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu ()
    {
        Debug.Log("Go to menu");
        SceneManager.LoadScene(0); // WIP Hard Code for now
    }

    public void SoftResetLevel()
    {
        LevelManager.level.SoftResetLevel();
    }

}
