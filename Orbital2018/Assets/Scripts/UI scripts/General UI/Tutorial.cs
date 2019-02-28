using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerClickHandler {

    [Serializable]
    public class TutorialPointers
    {
        [TextArea] public string text;
        public GameObject image;
    }
    
    [TextArea] public string[] thingsToSay;
    public TutorialPointers[] pointers;
    [TextArea] public string goToNext;
    public TextMeshProUGUI textMeshPro;
    public GameObject closeButton;

    private int index;
    private bool waitingInput;
    private GameObject activeImage;

    void Start()
    {
        if (!System.IO.File.Exists(MainPlayerStats.path))
        {
            closeButton.SetActive(false);
        }
        StartCoroutine("Say", 0f);
    }

    IEnumerator SayTutorial()
    {
        for (int i = 0; i < pointers.Length; i++)
        {
            string statement = "";
            string pointerStr = pointers[i].text;
            if (pointers[i].image != null)
            {
                pointers[i].image.SetActive(true);
                activeImage = pointers[i].image;
            }
            for (int j=0; j<pointerStr.Length; j++)
            {
                statement += pointerStr[j];
                textMeshPro.text = statement;
                yield return new WaitForSecondsRealtime(0.025f);
            }
            Debug.Log(statement);
            statement += goToNext;
            textMeshPro.text = statement;
            waitingInput = true;
            while (waitingInput)
            {
                yield return new WaitForEndOfFrame();
            }
            if (activeImage != null) activeImage.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Destroy(gameObject);
    }

    IEnumerator Say()
    {
        for (int i = 0; i < thingsToSay.Length; i++)
        {
            string statement = "";
            for (int j = 0; j < thingsToSay[i].Length; j++)
            {
                statement += thingsToSay[i][j];
                textMeshPro.text = statement;
                yield return new WaitForSecondsRealtime(0.025f);
            }
            statement += goToNext;
            textMeshPro.text = statement;
            waitingInput = true;
            while (waitingInput)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
        StartCoroutine("SayTutorial");
    }

    public void OnPointerClick (PointerEventData data)
    {
        waitingInput = false;
    }
}
