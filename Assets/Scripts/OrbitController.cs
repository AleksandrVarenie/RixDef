using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public Transform target; // ��'��� ������� ����� ���� ������ ��������
    public float radius; // ����� ����
    public float orbitSpeed; // �������� ���������� �� ����

    private Vector3 targetPosition; // �������, �� ��� ���� ��������� ���������

    void Update()
    {
        // ���������� ������� �������� �� ���
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetDirection = mousePosition - target.position;
        targetDirection.z = 0;
        targetDirection = targetDirection.normalized;

        // ���������� �������, �� ��� ���� ��������� ���������
        targetPosition = target.position + targetDirection * radius;

        // ���������� ��������� �� ���� �������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, orbitSpeed * Time.deltaTime);

        // ��������� ��������� � ������� ����
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
