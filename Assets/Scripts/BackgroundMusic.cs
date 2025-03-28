using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}