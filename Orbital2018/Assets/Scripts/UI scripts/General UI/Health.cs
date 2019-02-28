using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public Image[] hearts;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerStats.Lives)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
