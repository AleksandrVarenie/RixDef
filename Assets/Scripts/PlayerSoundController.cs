using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public List<AudioClip> movementSounds; // список звуків для відтворення при русі персонажа
    public float movementSoundInterval = 0.5f; // інтервал між відтворенням звуків при русі персонажа
    public float movementDistance = 1.0f; // дистанція, яку повинен пройти персонаж, щоб програти звук

    private float lastMovementSoundTime; // час останнього відтворення звуку руху
    private float totalDistance; // загальна пройдена дистанція

    private AudioSource audioSource;

    private Vector2 lastPosition; // попередня позиція персонажа

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
        AudioClip randomSound = movementSounds[Random.Range(0, movementSounds.Count)]; // випадковий звук зі списку
        audioSource.clip = randomSound;
        audioSource.Play();
    }
}
