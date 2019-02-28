using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    [Header("Spawn params")]
    private float cooldown;

    public WaveManager WM;

    // ObjectPooler objectPooler;

    [SerializeField]
    private int waveIndex;
    private IEnumerator spawnCoroutine;

    [HideInInspector]public List<string> enemies = new List<string>() { "Please Select" };

    void Start() {
        // objectPooler = ObjectPooler.Instance;
        //WM.totalCount = 0;
        //WM.currCount = 0;
        spawnCoroutine = SpawnWaves();
        StartCoroutine(spawnCoroutine);
        for (int i = 0; i < WM.wave.Length; i++)
        {
            for (int j = 0; j < WM.wave[i].spawnInstructions.Length; j++)
            {
                GameObject enemyPrefab = WM.wave[i].spawnInstructions[j].spawnPrefab;
                if (!enemies.Contains(enemyPrefab.tag))
                    enemies.Add(enemyPrefab.tag);
            }
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(WM.timeToStart);
        WaveManager.Wave[] waves = WM.wave;
        while (waveIndex < waves.Length)
        {
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(waves[waveIndex].timeToNextWave);
            waveIndex++;
        }
    }

    IEnumerator SpawnWave()
    {
        WaveManager.Wave.SpawnInstruction[] currentWaveSI = WM.wave[waveIndex].spawnInstructions;
        for (int j = 0; j < currentWaveSI.Length; j++)
        {
            GameObject prefab = currentWaveSI[j].spawnPrefab;
            Spawn(prefab);
            yield return new WaitForSeconds(currentWaveSI[j].timeToNextSpawn);
        }
    }

    void Spawn(GameObject prefab) {
        //Debug.Log(Time.time);
        // WM.totalCount++;
        Instantiate(prefab, transform.position, transform.rotation);
        /*
        GameObject enemy = ObjectPool.instance.GetPooledObject(1);
        if (enemy == null)
        {
            return;
        }
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
        enemy.SetActive(true);
        */
    }

    public void ResetWaveNum()
    {
        StopAllCoroutines();
        waveIndex = 0;
        spawnCoroutine = SpawnWaves();
        StartCoroutine(spawnCoroutine);
    }

    public int GetWaveIndex()
    {
        return waveIndex;
    }
}
