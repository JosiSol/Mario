using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framesPerSecond = 1f/6f; // 6 frames per second

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framesPerSecond, framesPerSecond);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Animate()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= sprites.Length)
        {
            currentSpriteIndex = 0;
        }
        if (currentSpriteIndex >= 0 && currentSpriteIndex < sprites.Length){
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        } 
    }
}
