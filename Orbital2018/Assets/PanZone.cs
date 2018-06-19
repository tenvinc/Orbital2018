using UnityEngine;
using UnityEngine.EventSystems;

public class PanZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string panType = "East";

    [Header("Cardinal Directions")]
    public string North = "North";
    public string South = "South";
    public string East = "East";
    public string West = "West";


    private bool inZone;
    private bool cameraLocked;
    private CameraController mainCam;
    private Transform image;


    void Awake()
    {
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        mainCam = camList[0].GetComponent<CameraController>();
        inZone = false;
        cameraLocked = false;
        image = transform.GetChild(0);
        image.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!cameraLocked) image.gameObject.SetActive(true);
        inZone = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!cameraLocked) image.gameObject.SetActive(false);
        inZone = false;
    }

    void Update()
    {
        if (inZone && !cameraLocked)
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

    public void LockCamera()
    {
        Debug.Log("lock cam");
        cameraLocked = !cameraLocked;
    }
}
