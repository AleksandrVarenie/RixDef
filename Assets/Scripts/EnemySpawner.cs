using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;
    public float spawnRadius;
    public float minSpeed;
    public float maxSpeed;
    public float initialWaveDelay;
    public int enemiesPerWave;
    public float timeBetweenWaves;
    public float timeBetweenSpawns; // час між спавнами ворогів у хвилі

    public TextMeshProUGUI text;

    private float waveTimer;
    private int enemiesRemaining;
    public int liveEnemiesCount; // лічильник живих противників
    private Transform[] spawnPoints;

    private int waveTime = 0;
    
    void Start()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new Transform[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform;
        }

        waveTimer = initialWaveDelay;
        enemiesRemaining = 0;
        liveEnemiesCount = 0; // ініціалізуємо лічильник живих противників
        text.SetText(waveTime.ToString());
    }

    void Update()
    {
        if (enemiesRemaining == 0 && liveEnemiesCount == 0) // зупиняємо хвилю, коли всі противники вбиті
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                StartWave();
                waveTimer = timeBetweenWaves;
            }
        }
        if (waveTime == 5)
        {
            SceneManager.LoadScene(3);
        }
        Debug.Log(waveTime);
    }

    void StartWave()
    {
        waveTime++;
        enemiesPerWave += 4;
        enemiesRemaining = enemiesPerWave;
        StartCoroutine(SpawnEnemies()); // запускаємо корутину SpawnEnemies()
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesRemaining > 0)
        {
            SpawnEnemy();
            enemiesRemaining--;
            text.SetText(waveTime.ToString());
            yield return new WaitForSeconds(timeBetweenSpawns); // чекаємо деякий час перед спавном наступного ворога
        }
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnIndex].position;

        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemyMovement = enemyObject.GetComponent<EnemyController>();
        if (enemyMovement != null)
        {
            enemyMovement.target = target;
            enemyMovement.speed = Random.Range(minSpeed, maxSpeed);
            enemyMovement.OnDeath += OnEnemyDeath; // підпис
            liveEnemiesCount++; // збільшуємо лічильник живих противників
        }
    }

    void OnEnemyDeath()
    {
        liveEnemiesCount--; // зменшуємо лічильник живих противників, коли противник помирає
    }
}