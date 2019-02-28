using UnityEngine;
using TMPro;

public class Money : MonoBehaviour {

    private TextMeshProUGUI coinsCounter;

    private void Start ()
    {
        coinsCounter = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        coinsCounter.text = "$" + PlayerStats.Money.ToString();
    }

}
