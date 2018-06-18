using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    [Header("Spawn params")]
    private float cooldown;
    public float timeBetweenWaves = 1f;
    public float initialWait = 0.5f;
    public float timeBetweenSpawns = 0.5f;

    [Header("Enemy prefabs")]
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    [SerializeField]
    private int waveNum;

    void Start() {
        cooldown = initialWait;
        waveNum = 0;
    }

    void Update() {
        if (cooldown <= 0f) {
            StartCoroutine(SpawnWave());
            cooldown = timeBetweenWaves;
        }
        cooldown -= Time.deltaTime;
    }

    IEnumerator SpawnWave() {
        waveNum++;
        for (int i = 0; i < waveNum; i++) {
            Spawn();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        SpawnBoss();
    }
    // Currently this spawns only 1 type of enemy and it is hardcoded in
    // Can change this to receive another input to determine who to spawn
    void Spawn() {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    void SpawnBoss ()
    {
        Instantiate(bossPrefab, transform.position, transform.rotation);
    }
}
