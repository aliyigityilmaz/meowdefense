using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public float spawnRate = 2f;
    public float nextSpawn = 0.0f;

    [Header("Spawn Points")]
    public GameObject[] spawnPoints;

    public int maxEnemies = 10;

    [Header("Wave System")]
    public float waveCountdown = 30f;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveCountdown -= Time.deltaTime;
        if(waveCountdown <= 0)
        {
            waveNumber++;
            maxEnemies += 5;
            waveCountdown = 30f;
        }
        
        if(Time.time > nextSpawn)
        {
            
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                nextSpawn = Time.time + spawnRate;
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                int enemyIndex = Random.Range(0, 3);
                if (enemyIndex == 0)
                {
                    Instantiate(enemy1, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
                }
                else if (enemyIndex == 1)
                {
                    if (waveNumber < 2) return;
                    Instantiate(enemy2, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
                }
                else
                {
                    if (waveNumber < 3) return;
                    Instantiate(enemy3, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
                }

            }
        }
    }
}
