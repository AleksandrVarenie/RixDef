using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip firstSong;
    public AudioClip secondSong;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = firstSong;
        audioSource.Play();
        StartCoroutine(WaitForSongToEnd());
    }

    IEnumerator WaitForSongToEnd()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioSource.clip = secondSong;
        audioSource.loop = true;
        audioSource.Play();
    }
}