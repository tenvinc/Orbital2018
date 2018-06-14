using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 velocity = new Vector3 (0, 0, 0);
    public float gravity = 10f;

    public void Initialise(Vector3 _velocity)
    {
        velocity = _velocity;
        Debug.Log("starting velocity in y is " + velocity.y);
    }

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        Debug.Log("velocity.y is " + velocity.y);
        transform.Translate(velocity*Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("game object is destroyed");
        Destroy(gameObject);
    }
}
