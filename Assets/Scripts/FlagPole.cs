using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 7f;
    public new AudioSource audio;
    public AudioClip levelCompleteSound;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (BackgroundMusic.Instance != null)
            {
                BackgroundMusic.Instance.StopMusic();
            }
            audio.PlayOneShot(levelCompleteSound);
            StartCoroutine(Move(flag, poleBottom.position));
            StartCoroutine(LevelComplete(collision.transform));
        }
    }
    private IEnumerator LevelComplete(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return Move(player, poleBottom.position);
        yield return Move(player, player.position + Vector3.right);
        yield return Move(player, player.position + Vector3.right + Vector3.down);
        yield return Move(player, castle.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.PlayMusic(MusicType.Background);
        }
        // Implement this to change levels when flagpole is touched
        yield return new WaitForSeconds(1f);
        GameManager.Instance.NextLevel();
    }
    private IEnumerator Move(Transform subject, Vector3 position)
    {
        while (Vector3.Distance(subject.position, position) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, position, speed * Time.deltaTime);
            yield return null;
        }
        subject.position = position;
    }
}
