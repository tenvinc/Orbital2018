using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class InterfaceManager : MonoBehaviour {

    public Transform visibleUI;
    public Transform hiddenUI;
    public Transform visibleCSUI;

    public UnityEvent DisableUI;
    public UnityEvent EnableUI;

    public static InterfaceManager ui;
    public static bool gamePaused;
    public GameObject pauseMenu;
    [HideInInspector]public Transform activeCSUser;
    public GameObject towerMenu;

    public GameObject PauseText;
    public GameObject ResumeText;
    public GameObject Tooltip;
    private TextMeshProUGUI TooltipText;

    public GameObject lockIcon;

    [HideInInspector]public bool clickSourceKnown;
    [HideInInspector]public TurretNode selectedNode;
    [HideInInspector]public bool isUIOverride;

    // For debugging purposes dun touch unless rescaling the ui elements
    private readonly float minYPercent = 0.21f;
    private readonly float maxXPercent = 0.55f;

    private Dictionary<string, Transform> CSdict;

    private bool stageStarted;

    void Awake() {
        if (ui != null)
        {
            Debug.Log("More than 1 UI manager in scene");
        }
        else { 
        
            ui = this; 
        }
        CSdict = new Dictionary<string, Transform>();
        TooltipText = Tooltip.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start() {

        gamePaused = true;
        stageStarted = false;
        Time.timeScale = 0f;
        clickSourceKnown = false;
    }

    void Update() {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused)
            {
                if (!stageStarted)
                {
                    bool isActive = pauseMenu.activeSelf;
                    pauseMenu.SetActive(!isActive);
                }   
                else if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                    Resume();
                }
                else
                {
                    pauseMenu.SetActive(true);
                }
            }
            else
            {
                pauseMenu.SetActive(true);
                Pause();
            }
        }
        if (Input.GetMouseButtonDown(0))
            CheckAirClick();
        if (Input.GetMouseButtonDown(1))
        {
            ShowTowerMenu();
        }
    }

    void CheckAirClick()
    {
        if (Input.mousePosition.y < minYPercent * Screen.height || Input.mousePosition.x > maxXPercent * Screen.width ||
            isUIOverride)
        {
            return;
        }
        if (!clickSourceKnown)
        {
            if (selectedNode != null) selectedNode.ResetSelection();
            BuildManager.bm.ResetTurretToBuild();
            BuildManager.bm.ResetDupTurret();
            towerMenu.SetActive(false);
        }
        else 
        {
            clickSourceKnown = false;
        }
    }

    void ShowTowerMenu()
    {
        if (selectedNode == null || selectedNode.turret == null) return;
        towerMenu.SetActive(true);
        towerMenu.GetComponent<RectTransform>().position = Input.mousePosition;
        BuildManager.bm.ResetTurretToBuild();
    }

    public void StartStage() {
        if (stageStarted) return;
        // NEW
        PlayerStats.Attempt++;
        GameObject[] Lines = GameObject.FindGameObjectsWithTag("Line");
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i].SetActive(false);
        }
        stageStarted = true;
        gamePaused = false;
        Time.timeScale = 1f;
        DisableUI.Invoke();
        BuildManager.bm.SetTurretToBuild(null);
    }

    void Resume() {
        if (!stageStarted) {
            Debug.Log("Stage not yet started");
            return;
        }
        Time.timeScale = 1f;
        gamePaused = false;
        PauseText.SetActive(true);
        ResumeText.SetActive(false);
    }

    void Pause() {
        if (!stageStarted) {
            return;
        }
        Time.timeScale = 0f;
        gamePaused = true;
        PauseText.SetActive(false);
        ResumeText.SetActive(true);
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

    public void EnterNewValue (string _string, Transform _transform)
    {
        if (CSdict == null || CSdict.ContainsKey(_string))
        {
            return;
        }
        CSdict.Add(_string, _transform);
    }

    public Transform GetValue (string _string)
    {
        return CSdict[_string];
    }

    public void ResumeFromPauseMenu()
    {
        pauseMenu.SetActive(false);
        Resume();
    }

    public void ResetStage()
    {
        gamePaused = true;
        stageStarted = false;
        Time.timeScale = 0f;
        EnableUI.Invoke();
    }

    public void ShowTooltip(Vector3 position, IDescribable description)
    {
        Tooltip.SetActive(true);
        Tooltip.transform.position = position;
        TooltipText.text = description.GetDescription();
    }

    public void HideTooltip()
    {
        Tooltip.SetActive(false);
    }

}
