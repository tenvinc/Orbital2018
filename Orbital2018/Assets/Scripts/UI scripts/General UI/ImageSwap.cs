using UnityEngine;
using UnityEngine.UI;

public class ImageSwap : MonoBehaviour {

    public GameObject consoleImage;
    public GameObject codeshopImage;

	public void SwapImage(string label)
    {
        if (label == "console")
        {
            codeshopImage.SetActive(false);
            consoleImage.SetActive(true);
        }
    }
}
