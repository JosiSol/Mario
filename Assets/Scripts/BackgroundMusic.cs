using UnityEngine;

public enum MusicType
{
    None,
    Background,
    Underground
}

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip undergroundMusic;

    private MusicType currentMusic = MusicType.None;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(MusicType.Background);
    }

    public void PlayMusic(MusicType type)
    {
        if (type == currentMusic) return;

        StopMusic();

        switch (type)
        {
            case MusicType.Background:
                if (backgroundMusic != null)
                {
                    audioSource.clip = backgroundMusic;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                break;
            case MusicType.Underground:
                if (undergroundMusic != null)
                {
                    audioSource.clip = undergroundMusic;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                break;
        }

        currentMusic = type;
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
        currentMusic = MusicType.None;
    }
}