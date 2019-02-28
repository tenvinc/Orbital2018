using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour {

    public Slider Volume;

	// Use this for initialization
	void Start () {
        MainPlayerStats.instance.ReadData();
        Volume.value = MainPlayerStats.saveData.Volume;
    }

    public void BackToMenu()
    {
        MainPlayerStats.saveData.Volume = Volume.value;
        MainPlayerStats.instance.SaveData();
    }

}
