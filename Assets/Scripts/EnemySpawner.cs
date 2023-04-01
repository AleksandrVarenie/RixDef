using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;
    public float spawnRadius;
    public float spawnDelay;
    public float minSpeed;
    public float maxSpeed;
    public int waveSize;
    public float waveDelay;

    private int enemiesRemainingInWave;
    private float nextSpawnTime;

    void Start()
    {
        enemiesRemainingInWave = waveSize;
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        if (enemiesRemainingInWave > 0 && Time.time > nextSpawnTime)
        {
            GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.target = target;
                enemyController.speed = Random.Range(minSpeed, maxSpeed);
            }
            enemiesRemainingInWave--;
            nextSpawnTime = Time.time + spawnDelay;
        }

        if (enemiesRemainingInWave == 0)
        {
            if (GameObject.FindWithTag("Enemy") == null)
            {
                enemiesRemainingInWave = waveSize;
                StartCoroutine(WaveDelay());
            }
        }
        Debug.Log(enemiesRemainingInWave);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        return transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(waveDelay);
    }

    public void EnemyDestroyed()
    {
        enemiesRemainingInWave++;
    }
}