using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public KeyCode key = KeyCode.S;
    public Vector3 enter = Vector3.down;
    public Vector3 exit = Vector3.zero;
    public Transform connection;
    public AudioSource pipeEnterSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (connection != null && collision.CompareTag("Player")) 
        {
            if (Input.GetKey(key))
            {
                StartCoroutine(Enter(collision.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        if (pipeEnterSound != null)
        {
            pipeEnterSound.Play();
        }

        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enterPos = transform.position + enter;
        Vector3 enterScale = Vector3.one * 0.5f;

        yield return Animate(player, enterPos, enterScale);
        yield return new WaitForSeconds(1f);

        Camera.main.GetComponent<SideScrolling>().setUnderground(true ? connection.position.y < 0f : false);
        if (exit != Vector3.zero)
        {
            player.position = connection.position - exit;
            yield return Animate(player, connection.position + exit, Vector3.one);
        }
        else 
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Animate(Transform player, Vector3 endPos, Vector3 endScale)
    {
        float elapsed = 0;
        float duration = 1f;

        Vector3 startPos = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            player.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            player.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        player.position = endPos;
        player.localScale = endScale;
    }
}
