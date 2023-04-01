using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;
    public float spawnDelay;
    public float spawnRadius;
    public float minSpeed;
    public float maxSpeed;
    public float initialWaveDelay;
    public int enemiesPerWave;

    public float timeBetweenWaves; // затримка між хвилями

    private float spawnTimer;
    private float waveTimer;
    private int enemiesRemaining;

    void Start()
    {
        waveTimer = initialWaveDelay;
        enemiesRemaining = 0;
    }

    void Update()
    {
        if (enemiesRemaining == 0)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                StartWave();
                waveTimer = timeBetweenWaves;
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = spawnDelay;

                enemiesRemaining--;

                if (enemiesRemaining == 0)
                {
                    waveTimer = timeBetweenWaves;
                }
            }
        }
    }

    void StartWave()
    {
        enemiesRemaining = enemiesPerWave;
        spawnTimer = 0;
    }

    void SpawnEnemy()
    {
        float spawnAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 spawnPosition = new Vector3(Mathf.Sin(spawnAngle), Mathf.Cos(spawnAngle), 0f) * spawnRadius;
        spawnPosition += transform.position;

        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        float enemySpeed = Random.Range(minSpeed, maxSpeed);
        EnemyController enemyMovement = enemyObject.GetComponent<EnemyController>();
        if (enemyMovement != null)
        {
            enemyMovement.target = (target);
            enemyMovement.speed = (enemySpeed);
        }
    }
}