using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public Transform target; // ��'��� ������� ����� ���� ������ ��������
    public float radius; // ����� ����
    public float orbitSpeed; // �������� ���������� �� ����

    private Vector3 targetPosition; // �������, �� ��� ���� ��������� ���������

    public float pushForce = 10f; // ���� �������������

    void Update()
    {
        // ���������� ������ �� ������ �� �����
        float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), target.position);

        // ���� ����� �������� �� ������ ����, �� ���������� ��������� � �������� �����
        if (distance > radius)
        {
            Vector3 targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position;
            targetDirection.z = 0;
            targetDirection = targetDirection.normalized;
            targetPosition = target.position + targetDirection * radius;
        }
        // ������ ���������� ��������� �� ������ ���� � ��������� �� ������ �� �����
        else
        {
            Vector3 targetDirection = transform.position - target.position;
            targetDirection.z = 0;
            targetDirection = targetDirection.normalized;
            targetPosition = target.position + targetDirection * distance;
        }

        // ���������� ��������� �� ���� �������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, orbitSpeed * Time.deltaTime);

        // �������� ��������� � ������� �����, ��� � ����������� �������� � ��������� �� ������ �� ��
        Vector3 lookDirection = target.position - transform.position;
        float angle = Vector3.SignedAngle(Vector3.right, lookDirection, Vector3.forward);
        if (distance >= radius)
        {
            angle += 180f;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
