using UnityEngine;
using UnityEngine.EventSystems;

public class PanZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string panType = "East";
    public bool inZone = false;
    private CameraController mainCam;

    [Header("Cardinal Directions")]
    public string North = "North";
    public string South = "South";
    public string East = "East";
    public string West = "West";


    void Awake()
    {
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        mainCam = camList[0].GetComponent<CameraController>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inZone = true;
        Debug.Log("InZone");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inZone = false;
        Debug.Log("OutZone");
    }

    void Update()
    {
        if (inZone)
        {
            if (panType == "North")
            {
                mainCam.PanUp();
            }
            else if (panType == "South")
            {
                mainCam.PanDown();
            }
            else if (panType == "West")
            {
                mainCam.PanLeft();
            }
            else if (panType == "East")
            {
                mainCam.PanRight();
            }
        }
    }
}
