using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKiller : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamageFromWall(100);
        }
    }
}
