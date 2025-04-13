using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public new AudioSource audio;
    public AudioClip deathClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (BackgroundMusic.Instance != null)
            {
                BackgroundMusic.Instance.StopMusic();
            }
            audio.PlayOneShot(deathClip);
            collision.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(4f);
        }
        else 
        {
            Destroy(collision.gameObject);
        }
    }
}
