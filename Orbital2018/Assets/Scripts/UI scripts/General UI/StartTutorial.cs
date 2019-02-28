using UnityEngine;

public class StartTutorial : MonoBehaviour {

    public Tutorial tutorial;

    private void Start()
    {
        StartCoroutine("Tutorial", 0.5f);
    }

    void Tutorial()
    {
        if (tutorial == null) return;
        tutorial.gameObject.SetActive(true);
    }
}
