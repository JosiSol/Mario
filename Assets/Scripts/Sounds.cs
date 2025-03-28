using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip coin;
    public AudioClip death;
    public AudioClip endlevel;
    public AudioClip jump;
    public AudioClip oneUp;
    public AudioClip pipe;
    public AudioClip powerup;
    public AudioClip powerdown;
    public AudioClip star;
    
    public AudioSource audioSource; // Assign this in the Inspector or via script
    public AudioSource backgroundMusic; // Assign this in the Inspector or via script
    public AudioClip normalMusic;
    public AudioClip undergroundMusic;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (backgroundMusic == null)
            backgroundMusic = gameObject.AddComponent<AudioSource>();

        backgroundMusic.clip = normalMusic;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Jump sound
        {
            PlaySound(jump);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void EnterUnderground()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = undergroundMusic;
        backgroundMusic.Play();
    }

    public void ExitUnderground()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = normalMusic;
        backgroundMusic.Play();
    }
}