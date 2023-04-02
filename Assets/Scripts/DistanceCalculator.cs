using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public Transform player; // головний персонаж або інша точка, від якої розраховується відстань
    public float maxVolumeDistance = 10f; // максимальна відстань, на якій звук ворога буде максимально гучним
    public float minVolumeDistance = 1f; // мінімальна відстань, на якій звук ворога буде максимально тихим

    void Update()
    {
        // динамічно отримуємо всіх ворогів на сцені
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // розраховуємо відстань до всіх ворогів
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);

            // знаходимо компонент AudioSource на об'єкті ворога
            AudioSource audioSource = enemy.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                // знаходимо гучність в залежності від відстані до головного персонажа
                float volume = Mathf.Clamp01(1f - (distance - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));

                // встановлюємо нову гучність
                audioSource.volume = volume;
            }
        }
    }

}
