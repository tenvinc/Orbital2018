using UnityEngine;

public class InterfaceManager : MonoBehaviour {

    public static InterfaceManager ui;
    public Transform visibleUI;
    public Transform hiddenUI;

    void Awake() {
        if (ui != null) {
            Debug.Log("More than 1 build manager in scene");
        }
        else ui = this;
    }

}
