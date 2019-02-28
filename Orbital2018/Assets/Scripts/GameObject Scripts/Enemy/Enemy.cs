using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour {

    public float startHealth = 100f;
    public float startSpeed = 10f;

    [Header("Enemy Attributes")]
    public float speed;
    [HideInInspector]public float health;
    public int value = 50;

    // Buffing variables
    private bool isBuffed;
    private float buffProportion;

    protected float buffCD;

    [Header("Unity Stuff")]
    public Image healthBar;
    public GameObject DropLootPrefab;
    public GameObject FloatingTextPrefab;
    public WaveManager WM;

    [Header("Particle FX")]
    public ParticleSystem skillFx = null;
    public GameObject dieFX = null;
    public AudioSource AudioFx = null;
    // public ParticleSystem buffFX;

    protected float skillCD;

    public EnemySkills enemySkills;
    public bool isDead;

    void Start ()
    {
        health = startHealth;
        speed = startSpeed;
        skillCD = enemySkills.additionalTime;
        isBuffed = false;
        buffProportion = 1f;
        isDead = false;
    }


    public void TakeDamage (float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
            Die();
    }

    protected void Die ()
    {
        PlayerStats.Money += value;
        PlayerStats.moneyEarned += value;

        /*
        GameObject DropLoot = (GameObject)Instantiate(DropLootPrefab, transform.position, Quaternion.identity);
        Destroy(DropLoot, 5f);
        */

        GameObject go = (GameObject)Instantiate(FloatingTextPrefab, transform.position, Quaternion.Euler(new Vector3(40, 0 , 0)));
        go.GetComponent<TextMeshPro>().text = "+$" + value.ToString();

        isDead = true;
        Instantiate(dieFX, transform.position, transform.rotation);
        Destroy(gameObject);
        LevelManager.level.currCount++;
        PlayerStats.monstersKilled++;
        // gameObject.SetActive(false);

    }

    private void Update()
    {
        // Debug.Log(skillCD);
        if (skillCD <= 0)
        {
            //Debug.Log("Activate Skill");
            enemySkills.TriggerEnemySkill(transform);
            PlayFX();
            skillCD = enemySkills.cooldownTime;
        }
        skillCD -= Time.deltaTime;
        // Debug.Log(health);
        /*
        if (isBuffed)
        {
            if (buffCD <= 0)
            {
                resetBuff();
            }
            buffCD -= Time.deltaTime;
        }
        */
    }

    public void getBuff(float healthConstant)
    {
        if (!isBuffed)
        {
            //Debug.Log("Got buffed");
            buffProportion *= healthConstant;
            health *= buffProportion;
            isBuffed = true;
            // buffCD = buffCooldown;
            // buffFX.Play();
        }
    }

    public void MakeInsane(float speedBuffConstant)
    {
        if (!isBuffed)
        {
            speed *= speedBuffConstant;
            isBuffed = true;
        }
    }

    public void resetBuff()
    {
        // isBuffed = false;
        //Debug.Log("Reset buff");
        // buffProportion = 1f;
        // buffFX.Stop();
    }

    void PlayFX()
    {
        if (skillFx != null) skillFx.Play();
        if (AudioFx != null) AudioFx.Play();
    }

}
