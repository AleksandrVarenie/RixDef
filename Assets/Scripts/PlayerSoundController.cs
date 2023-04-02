using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public List<AudioClip> movementSounds; // ������ ����� ��� ���������� ��� ��� ���������
    public float movementSoundInterval = 0.5f; // �������� �� ����������� ����� ��� ��� ���������
    public float movementDistance = 1.0f; // ���������, ��� ������� ������ ��������, ��� �������� ����

    private float lastMovementSoundTime; // ��� ���������� ���������� ����� ����
    private float totalDistance; // �������� �������� ���������

    private AudioSource audioSource;

    private Vector2 lastPosition; // ��������� ������� ���������

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;
        totalDistance += Vector2.Distance(currentPosition, lastPosition);
        lastPosition = currentPosition;

        if (totalDistance >= movementDistance)
        {
            PlayRandomSound();
            totalDistance = 0;
        }
    }

    private void PlayRandomSound()
    {
        AudioClip randomSound = movementSounds[Random.Range(0, movementSounds.Count)]; // ���������� ���� � ������
        audioSource.clip = randomSound;
        audioSource.Play();
    }
}
