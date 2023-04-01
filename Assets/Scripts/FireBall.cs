using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float fireballDamage = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(fireballDamage);
        }
    }
}
