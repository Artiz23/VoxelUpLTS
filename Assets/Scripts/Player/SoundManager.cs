using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource platformAudioSource;
    public AudioSource checkPointAudioSource;
    public AudioSource coinAudioSource;
    public AudioSource deathAudioSource;

    private void Awake()
    {
        LoadAudioData(platformAudioSource);
        LoadAudioData(checkPointAudioSource);
        LoadAudioData(coinAudioSource);
        LoadAudioData(deathAudioSource);
    }

    private void LoadAudioData(AudioSource audioSource)
    {
        if (audioSource.clip != null)
        {
            audioSource.clip.LoadAudioData();
        }
    }

    public void PlayPlatformSound(float volume = 1.0f)
    {
        platformAudioSource.PlayOneShot(platformAudioSource.clip, volume);
    }

    public void PlayCheckPointSound(float volume = 1.0f)
    {
        checkPointAudioSource.PlayOneShot(checkPointAudioSource.clip, volume);
    }

    public void PlayCoinSound(float volume = 1.0f)
    {
        coinAudioSource.PlayOneShot(coinAudioSource.clip, volume);
    }

    public void PlayDeathSound(float volume = 1.0f)
    {
        deathAudioSource.PlayOneShot(deathAudioSource.clip, volume);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreZone"))
        {
            // Воспроизводить звук платформы при входе в ScoreZone
            PlayPlatformSound();
        }
        else if (other.CompareTag("CheckPoint"))
        {
            // Воспроизводить звук контрольной точки при входе в CheckPoint
            PlayCheckPointSound();
        }
    }
}
