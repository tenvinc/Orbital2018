using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptsFont : MonoBehaviour {

    private TextMeshProUGUI attemptsCounter;

    // Use this for initialization
    void Start () {
        attemptsCounter = GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {
        attemptsCounter.text = "ATTEMPTS" + " " + PlayerStats.Attempt;
	}
}
