using UnityEngine;

public class Projectile : MonoBehaviour {

    public Rigidbody rb;
    public float _gravity = 10f;
    private float gravity;

    public void Initialise(Vector3 _velocity)
    {
        rb.velocity = _velocity;
        gravity = _gravity;
    }

    void FixedUpdate()
    {
        rb.AddForce(0f, -gravity, 0f, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("game object is destroyed");
        Destroy(gameObject);
    }
}
