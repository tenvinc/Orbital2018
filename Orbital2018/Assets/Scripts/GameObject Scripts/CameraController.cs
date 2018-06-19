using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool freeToPan = true;

    [Header("Camera Attributes")]
    public float panSpeed = 30f;
    public float zoomSpeed = 10f;

    [Header("Camera Restrictions")]
    public float thresholdFromBorder = 10f;
    public float minY = 10f;
    public float maxY = 80f;

    [Header("Map Boundaries")]
    public float planeY = 2f;
    public Transform topLeft;
    public Transform topRight;
    public Transform btmLeft;
    public Transform btmRight;

    // make public for debugging
    private Vector3 TL;
    private Vector3 TR;
    private Vector3 BL;
    private Vector3 BR;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update() {
        // Can change the key next time
        if (Input.GetAxis("Camera")>0) 
            freeToPan = !freeToPan;

        if (!freeToPan) 
            return;
        // TODO: Add clamps to clamp the x,y,z for the player
        if (Input.GetAxis("Vertical") > 0) {
            PanUp();
        }
        else if (Input.GetAxis("Vertical") < 0) {
            PanDown();
        }
        else if (Input.GetAxis("Horizontal") < 0) {
            PanLeft();
        }
        else if (Input.GetAxis("Horizontal") > 0) {
            PanRight();
        }

        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        float absScrollVal = Mathf.Abs(scrollVal);
        // Scroll wheel to zoom in and out
        if (scrollVal > 0) 
            transform.Translate(Vector3.forward * absScrollVal * zoomSpeed * 1000 * Time.deltaTime);
        else if (scrollVal < 0)
            transform.Translate(Vector3.back * absScrollVal * zoomSpeed * 1000 * Time.deltaTime);

    }

    public void PanUp()
    {
        GetTopLeftBound();
        GetTopRightBound();
        if (TL.z >= topLeft.position.z || TR.z >= topRight.position.z) return;
        transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
    }

    public void PanDown()
    {
        GetBtmLeftBound();
        GetBtmRightBound();
        if (BL.z <= btmLeft.position.z || BR.z <= btmRight.position.z) return;
        transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
    }

    public void PanLeft()
    {
        GetBtmLeftBound();
        GetTopLeftBound();
        if (BL.x <= btmLeft.position.x || TL.x <= topLeft.position.x) return;
        transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
    }

    public void PanRight()
    {
        GetTopRightBound();
        GetBtmRightBound();
        if (BR.x >= btmRight.position.x || TR.x >= topRight.position.x) return;
        transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
    }

    #region Compute where camera is looking at
    Vector3 GetTopLeftBound()
    {
        Vector3 pos = GetWorldPositionOnPlane(new Vector3(0, cam.scaledPixelHeight-1, 0), planeY);
        TL = pos;
        return pos;
    }

    Vector3 GetTopRightBound()
    {
        Vector3 pos = GetWorldPositionOnPlane(new Vector3(cam.scaledPixelWidth-1, cam.scaledPixelHeight - 1, 0), planeY);
        TR = pos;
        return pos;
    }

    Vector3 GetBtmLeftBound()
    {
        Vector3 pos = GetWorldPositionOnPlane(new Vector3(0, 0, 0), planeY);
        BL = pos;
        return pos;
    }

    Vector3 GetBtmRightBound()
    {
        Vector3 pos = GetWorldPositionOnPlane(new Vector3(cam.scaledPixelWidth-1, 0, 0), planeY);
        BR = pos;
        return pos;
    }

    Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
    #endregion
}
