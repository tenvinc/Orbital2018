using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy Attributes")]
    public float speed = 60f;
    public float health = 100f;
    public int value = 50;

    public void TakeDamage (float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die ()
    {
        PlayerStats.Money += value;
        Destroy(gameObject);
    }

}
