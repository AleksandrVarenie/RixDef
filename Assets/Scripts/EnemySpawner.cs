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
    public float timeBetweenSpawns; // ��� �� �������� ������ � ����

    public TextMeshProUGUI text;

    private float waveTimer;
    private int enemiesRemaining;
    public int liveEnemiesCount; // �������� ����� ����������
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
        liveEnemiesCount = 0; // ����������� �������� ����� ����������
        text.SetText(waveTime.ToString());
    }

    void Update()
    {
        if (enemiesRemaining == 0 && liveEnemiesCount == 0) // ��������� �����, ���� �� ���������� ����
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                StartWave();
                waveTimer = timeBetweenWaves;
            }
        }
        Debug.Log(waveTime);
    }

    void StartWave()
    {
        if (waveTime == 6)
        {
            SceneManager.LoadScene(3);
        }
        waveTime++;
        enemiesPerWave += 4;
        enemiesRemaining = enemiesPerWave;
        StartCoroutine(SpawnEnemies()); // ��������� �������� SpawnEnemies()
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesRemaining > 0)
        {
            SpawnEnemy();
            enemiesRemaining--;
            text.SetText(waveTime.ToString());
            yield return new WaitForSeconds(timeBetweenSpawns); // ������ ������ ��� ����� ������� ���������� ������
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
            enemyMovement.OnDeath += OnEnemyDeath; // �����
            liveEnemiesCount++; // �������� �������� ����� ����������
        }
    }

    void OnEnemyDeath()
    {
        liveEnemiesCount--; // �������� �������� ����� ����������, ���� ��������� ������
    }
}