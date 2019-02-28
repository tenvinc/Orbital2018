using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public Button[] LevelButtons;

    private void Start()
    {
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if (i + 1 > MainPlayerStats.saveData.levelReached)
            {
                LevelButtons[i].interactable = false;
            }
        }


    }

    public void Select (int levelIndex)
    {
        LevelChanger.instance.FadeToLevel(levelIndex);
    }

    public void Menu ()
    {
        SceneManager.LoadScene(0);
    }
}
