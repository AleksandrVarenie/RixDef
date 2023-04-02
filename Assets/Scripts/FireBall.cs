using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject owner;
    public float fireballDamage = 10f;

    public GameObject Light;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(fireballDamage);
        }
    }

    public float growthRate; // �������� ��������� ������ �������
    public float maxSize; // ������������ ����� �������
    public float lifetime; // ��� ����� �������

    public float currentSize; // �������� ����� �������
    public float age; // ���, �� ������� � ������� ��������� �������

    void Start()
    {
        age = 0f;
    }

    void Update()
    {
        age += Time.deltaTime;
        currentSize += growthRate * Time.deltaTime;

        if (currentSize > maxSize)
        {
            currentSize = maxSize;
        }

        transform.localScale = new Vector3(0.5f, currentSize, currentSize); //currentSize, currentSize);

        if (age >= lifetime)
        {
           // Light.GetComponent<FireBallLight>().fireBallDead = true;
            Destroy(gameObject);
        }
    }
}
