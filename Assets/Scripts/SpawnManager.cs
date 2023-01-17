using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float min = -9.0f;
    private float max = 9.0f;
    public int waveNumber = 1;
    public int enemyCount;
    public GameObject powerPrefab;

    // Start is called before the first frame update
    void Start()
    {

        SpawnEnemyWaves(waveNumber);
        Instantiate(powerPrefab, GenerateSpawnPosition(), powerPrefab.transform.rotation);
    }
    void SpawnEnemyWaves(int enemiesToSpawn)
    {
        for (int i=0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWaves(waveNumber);
            Instantiate(powerPrefab, GenerateSpawnPosition(), powerPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(min, max);
        float spawnZ = Random.Range(min, max);
        Vector3 respawn = new Vector3(spawnX, 0, spawnZ);
        return respawn;
    }
 
}
