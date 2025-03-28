using System.Collections;
using UnityEngine;

public class BlockItems : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        circleCollider.enabled = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;
        
        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while (elapsed < duration){
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            
            yield return null;
        }

        transform.localPosition = endPosition;

        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        circleCollider.enabled = true;
        boxCollider.enabled = true;
    }
}
