using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    public AudioClip lowEnemiesMusic;
    public AudioClip highEnemiesMusic;
    public AudioClip breakMusic;
    public float fadeDuration = 1f;
    public float musicThreshold = 0.5f;
    private bool isPlayingBreakMusic = false;
    private EnemySpawner enemySpawner;
    private AudioSource audioSource;

    private AudioClip currentClip;
    private float targetVolume;
    private float fadeSpeed;

    void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        audioSource = GetComponent<AudioSource>();
        currentClip = lowEnemiesMusic;
        targetVolume = audioSource.volume;
        fadeSpeed = targetVolume / fadeDuration;
        audioSource.clip = currentClip;
        audioSource.Play();
    }

    void Update()
    {
        // if (isPlayingBreakMusic && enemySpawner.liveEnemiesCount >= enemySpawner.enemiesPerWave * musicThreshold)
        if (isPlayingBreakMusic || currentClip == lowEnemiesMusic && enemySpawner.liveEnemiesCount > 5)
        {
            isPlayingBreakMusic = false;
            currentClip = highEnemiesMusic;
            targetVolume = audioSource.volume;
            fadeSpeed = targetVolume / fadeDuration;
        }

        if (currentClip == highEnemiesMusic || currentClip == breakMusic && enemySpawner.liveEnemiesCount > 0)
        {
            currentClip = lowEnemiesMusic;
            targetVolume = audioSource.volume;
            fadeSpeed = targetVolume / fadeDuration;
        }

        if (enemySpawner.liveEnemiesCount <= 0 && !isPlayingBreakMusic)
        {
            currentClip = breakMusic;
            targetVolume = audioSource.volume;
            fadeSpeed = targetVolume / fadeDuration;
            isPlayingBreakMusic = true;
        }

        if (audioSource.clip != currentClip)
        {
            if (Mathf.Approximately(audioSource.volume, 0f))
            {
                audioSource.Stop();
                audioSource.clip = currentClip;
                audioSource.Play();
            }
            else if (audioSource.volume <= targetVolume)
            {
                audioSource.Stop();
                audioSource.clip = currentClip;
                audioSource.volume = targetVolume;
                audioSource.Play();
            }
            else
            {
                audioSource.volume -= fadeSpeed * Time.deltaTime;
            }
        }
    }
}