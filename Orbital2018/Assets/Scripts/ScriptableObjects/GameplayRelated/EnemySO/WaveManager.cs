using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WaveManager", menuName = "LevelSpawns/WaveManager")]
public class WaveManager : ScriptableObject {

    [Serializable]
    public class Wave 
    {

        [Serializable]
        public class SpawnInstruction
        {
            public GameObject spawnPrefab;
            public float timeToNextSpawn;
        }

        public SpawnInstruction[] spawnInstructions;
        public float timeToNextWave;
    }

    [Tooltip("Specify this list in order")]
    public Wave[] wave;
    public float timeToStart;
}
