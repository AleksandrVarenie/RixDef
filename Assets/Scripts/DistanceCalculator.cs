using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public Transform player; // �������� �������� ��� ���� �����, �� ��� ������������� �������
    public float maxVolumeDistance = 10f; // ����������� �������, �� ��� ���� ������ ���� ����������� ������
    public float minVolumeDistance = 1f; // �������� �������, �� ��� ���� ������ ���� ����������� �����

    void Update()
    {
        // �������� �������� ��� ������ �� ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // ����������� ������� �� ��� ������
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);

            // ��������� ��������� AudioSource �� ��'��� ������
            AudioSource audioSource = enemy.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                // ��������� ������� � ��������� �� ������ �� ��������� ���������
                float volume = Mathf.Clamp01(1f - (distance - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));

                // ������������ ���� �������
                audioSource.volume = volume;
            }
        }
    }

}
