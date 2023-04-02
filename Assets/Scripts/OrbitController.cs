using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public Transform target; // об'єкт навколо якого буде ходити персонаж
    public float radius; // радіус кола
    public float orbitSpeed; // швидкість переміщення по колу

    private Vector3 targetPosition; // позиція, до якої буде переміщено персонажа

    public float pushForce = 10f; // сила виштовхування

    void Update()
    {
        // Обчислення відстані між мишкою та ціллю
        float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), target.position);

        // Якщо мишка перебуває за межами кола, то перемістити персонажа в напрямку мишки
        if (distance > radius)
        {
            Vector3 targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position;
            targetDirection.z = 0;
            targetDirection = targetDirection.normalized;
            targetPosition = target.position + targetDirection * radius;
        }
        // Інакше перемістити персонажа по радіусу кола в залежності від відстані до мишки
        else
        {
            Vector3 targetDirection = transform.position - target.position;
            targetDirection.z = 0;
            targetDirection = targetDirection.normalized;
            targetPosition = target.position + targetDirection * distance;
        }

        // Перемістити персонажа до нової позиції
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, orbitSpeed * Time.deltaTime);

        // Обертати персонажа в сторону мишки, або в протилежний напрямок в залежності від відстані до неї
        Vector3 lookDirection = target.position - transform.position;
        float angle = Vector3.SignedAngle(Vector3.right, lookDirection, Vector3.forward);
        if (distance >= radius)
        {
            angle += 180f;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
