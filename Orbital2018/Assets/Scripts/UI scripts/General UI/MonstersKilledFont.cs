using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonstersKilledFont : MonoBehaviour {

    private TextMeshProUGUI monstersKilledCounter;
    private int totalMonstersKilled = 0;
    private int maxMonstersKilled = 0;

    public WaveManager[] L;

    // Use this for initialization
    void Start()
    {
        MainPlayerStats.instance.ReadData();
        monstersKilledCounter = GetComponent<TextMeshProUGUI>();
        if (MainPlayerStats.monstersKilled.Count > 0)
        {
            for (int i = 0; i < MainPlayerStats.monstersKilled.Count; i++)
            {
                totalMonstersKilled += MainPlayerStats.monstersKilled[i];
            }
        }
        for (int i = 0; i < L.Length; i++)
        {
            for (int j = 0; j < L[i].wave.Length; j++)
            {
                maxMonstersKilled += L[i].wave[j].spawnInstructions.Length;
            }
        }
        monstersKilledCounter.text = totalMonstersKilled.ToString() + "/" + maxMonstersKilled;
    }
}
