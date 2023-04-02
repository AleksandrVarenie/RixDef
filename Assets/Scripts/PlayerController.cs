using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float health = 1000.0f;

    public TextMeshProUGUI text;

    void Start()
    {
        text.SetText(health.ToString());
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        text.SetText(health.ToString());
        if (health <= 0)
        {
            // Гравець помер
            Destroy(gameObject);
            Debug.Log("Game over");
            SceneManager.LoadScene(2);
        }
    }
}