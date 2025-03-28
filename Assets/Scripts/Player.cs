using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;
    private DeathAnimation deathAnimation;
    private PlayerMovement playerMovement;
    private CapsuleCollider2D capsuleCollider;
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    private bool isAnimating = false;
    public bool starpower {get; private set;}

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    public void Hit()
    {
        if (isAnimating) return;
        
        if (!dead && !starpower)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Die();
            }
        }
    }
    private void Shrink()
    {
        isAnimating = true;
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;

        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }
    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;

        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }
    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }
            yield return null;
        }
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;

        activeRenderer.enabled = true;
        isAnimating = false;
    }
    private void Die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);   
    }
    public void StarPower(float duration = 8f)
    {
        starpower = true;

        StartCoroutine(StarPowerAnimation(duration));
    }
    private IEnumerator StarPowerAnimation(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration){
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
        
    }
}
