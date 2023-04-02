using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    // Оголошуємо подію на смерть противника
    public event System.Action OnDeath;

    public Transform target;
    public float speed;
    public float currentHealth = 1;

    public GameObject Coin;
    private bool haveCoin = false;

    private bool isWaiting = false;
    private float waitTime = 1f;
    private float waitTimer = 0f;

    private PlayerController player;

    void Start ()
    {
        player = FindObjectOfType<PlayerController>();
        coinVisibled();
    }

    void Update()
    {
        if (target != null)
        {
            if (!isWaiting)
            {
                Vector3 direction;
                if (!haveCoin)
                {
                    direction = (target.position - transform.position).normalized;
                } else
                {
                    direction = (transform.position - target.position).normalized;

                }
                transform.position += direction * speed * Time.deltaTime;
                transform.up = direction; // повертаємо ворога в сторону цілі
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
                   // speed *= -1f * 2f;
                    haveCoin = true;
                    coinVisibled();
                    if (player != null)
                    {
                        player.TakeDamage(10);
                    }
                }
            }
        }
    }

    public void coinVisibled ()
    {
        if (haveCoin)
        {
            Coin.SetActive(true);
        } else
        {
            Coin.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (haveCoin)
            {
                player.TakeDamage(-10);
            }
            if (OnDeath != null)
            {
                // Викликаємо всі методи, які підписалися на подію
                OnDeath();
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamageFromWall(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (OnDeath != null)
            {
                // Викликаємо всі методи, які підписалися на подію
                OnDeath();
            }
            Destroy(gameObject);
        }
    }

}