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
    private Transform[] spawnPoints; // список точок спавну

    void Start()
    {
        // Знаходимо всі пусті об'єкти з тегом "SpawnPoint" і додаємо їх в список
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new Transform[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform;
        }

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
        // Вибираємо випадкову точку спавну зі списку
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnIndex].position;

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
