using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if (deadSprite != null)
        {
           spriteRenderer.sprite = deadSprite; 
        }
    }
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().simulated = false;
        }

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }
    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;
        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 startPosition = transform.position;
        Vector3 velocity = Vector3.up * jumpVelocity;
        while(elapsed < duration)
        {
            Vector3 newPosition = startPosition;
            newPosition.y += velocity.y * elapsed + (0.5f * gravity * elapsed * elapsed);
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
