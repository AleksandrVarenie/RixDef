using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100.0f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Гравець помер
            Destroy(gameObject);
            Debug.Log("Game over");
        }
    }
}