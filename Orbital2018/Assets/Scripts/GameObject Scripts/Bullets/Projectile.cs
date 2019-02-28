using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Rigidbody rb;
    public float _gravity = 10f;
    public float damage = 50f;
    public float explosionRange = 20f;
    public TagMasterSO tagmasterso;
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
        Explosion();
        Destroy(gameObject);
    }

    void Explosion()
    {
        Collider[] withinRange = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider col in withinRange)
        {
            if (col.tag == tagmasterso.EnemyTag || col.tag == tagmasterso.BossTag)
            {
                Damage(col.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }
}
