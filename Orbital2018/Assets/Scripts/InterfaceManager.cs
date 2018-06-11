using UnityEngine;

public class InterfaceManager : MonoBehaviour {

    public Transform visibleUI;
    public Transform hiddenUI;
    public static InterfaceManager ui;
    public static bool gamePaused;

    private bool stageStarted;

    void Awake() {
        if (ui != null) {
            Debug.Log("More than 1 UI manager in scene");
        }
        else ui = this;
    }

    void Start() {
        gamePaused = true;
        stageStarted = false;
        Time.timeScale = 0f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void StartStage() {
        if (stageStarted) return;
        stageStarted = true;
        gamePaused = false;
        Time.timeScale = 1f;
    }

    void Resume() {
        if (!stageStarted) {
            Debug.Log("Stage not yet started");
            return;
        }
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause() {
        if (!stageStarted) {
            return;
        }
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void TogglePause() {
        if (!stageStarted) return;
        if (gamePaused) {
            Resume();
        }
        else {
            Pause();
        }
    }

}
