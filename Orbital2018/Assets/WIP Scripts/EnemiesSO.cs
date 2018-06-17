using UnityEngine;
using UnityEngine.UI;

public class EnemiesSO : ScriptableObject {

    [Header("Health")]
    public float startHealth = 100f;

    public float health;

    [Header("Speed")]
    public float startSpeed = 60f;

    public float speed;

    [Header("Enemies Worth")]
    public int startWorth = 50;

    public int worth;

    public Image healthBar;

    public void InitializeEnemies ()
    {
        health = startHealth;
        speed = startSpeed;
        worth = startWorth;
    }

}
