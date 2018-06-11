using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy Attributes")]
    public float speed = 60f;
    public float health = 100f;

    public void TakeDamage (float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die ()
    {
        Destroy(gameObject);
    }

}
