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
        transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
    }

    public void PanDown()
    {
        transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
    }

    public void PanLeft()
    {
        transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
    }

    public void PanRight()
    {
        transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
    }
}
