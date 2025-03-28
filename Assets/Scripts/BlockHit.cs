using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public Sprite emptyBlock;
    public int maxHits = -1;
    private bool animating;
    public GameObject item;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.TryGetComponent(out Player player))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }
    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        maxHits--;
        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }
        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        animating = true;
        Vector3 restingPosition = transform.localPosition;
        Vector3 targetPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, targetPosition);
        yield return Move(targetPosition, restingPosition);

        animating = false;
    }
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float time = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }

}
