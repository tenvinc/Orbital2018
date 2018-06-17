using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startHealth = 100f;

    [Header("Enemy Attributes")]
    public float speed = 60f;
    public float health;
    public int value = 50;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage (float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
            Die();
    }

    void Die ()
    {
        PlayerStats.Money += value;
        Destroy(gameObject);
    }

}
