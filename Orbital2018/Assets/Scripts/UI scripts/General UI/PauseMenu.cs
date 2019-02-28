using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

    public AudioMixerSnapshot bgmPaused;
    public AudioMixerSnapshot bgmUnpaused;

    private void OnEnable()
    {
        bgmPaused.TransitionTo(0.05f);
    }

    private void OnDisable()
    {
        bgmUnpaused.TransitionTo(0.05f);
    }

    public void ResumeFromMenu()
    {
        InterfaceManager.ui.ResumeFromPauseMenu();
    }

    public void SoftReset()
    {
        MainPlayerStats.instance.UpdateData();

        Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);
        MainPlayerStats.instance.SaveData();

        Debug.Log("MainPlayerStats.instance.savedata = " + MainPlayerStats.saveData.Stage.Count);
        LevelManager.level.SoftResetLevel();  
        ResumeFromMenu();
    }

    public void HardReset()
    {
        MainPlayerStats.instance.UpdateData();
        MainPlayerStats.instance.SaveData();
        LevelManager.level.HardResetLevel();
    }

    public void Quit()
    {
        MainPlayerStats.instance.UpdateData();
        MainPlayerStats.instance.SaveData();
        Application.Quit();
    }

    public void Menu()
    {
        MainPlayerStats.instance.UpdateData();
        MainPlayerStats.instance.SaveData();
        SceneManager.LoadScene(0); 
    }
}
