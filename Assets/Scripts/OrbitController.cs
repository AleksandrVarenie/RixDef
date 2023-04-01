using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public Transform target; // об'єкт навколо якого буде ходити персонаж
    public float radius; // радіус кола
    public float orbitSpeed; // швидкість переміщення по колу

    private Vector3 targetPosition; // позиція, до якої буде переміщено персонажа

    void Update()
    {
        // Обчислення вектора напрямку на миш
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetDirection = mousePosition - target.position;
        targetDirection.z = 0;
        targetDirection = targetDirection.normalized;

        // Обчислення позиції, до якої буде переміщено персонажа
        targetPosition = target.position + targetDirection * radius;

        // Переміщення персонажа до нової позиції
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, orbitSpeed * Time.deltaTime);

        // Обертання персонажа в сторону миші
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
