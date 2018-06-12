using UnityEngine;

public class TagManager : MonoBehaviour {

    public static TagManager tm;
    public string firetowerTag;
    public string enemyTag;
    public string dummyTag;
    public string conditionTag;

    void Awake()
    {
        if (tm != null)
        {
            Debug.Log("More than 1 tag manager in scene");
        }
        else tm = this;
    }
}
