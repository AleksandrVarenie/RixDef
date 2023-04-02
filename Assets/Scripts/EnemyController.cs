using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float currentHealth = 1;

    private bool isWaiting = false;
    private float waitTime = 3f;
    private float waitTimer = 0f;

    void Update()
    {
        if (target != null)
        {
            if (!isWaiting)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                //transform.up = direction; // повертаємо ворога в сторону цілі
                /* float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                if (Vector3.Distance(transform.position, target.position) < 0.1f)
                {
                    isWaiting = true;
                    waitTimer = waitTime;
                }
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    isWaiting = false;
                    speed *= -1f;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}