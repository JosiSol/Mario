using System.Collections;
using System.Linq;
using UnityEngine;
using static MusicType;

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
    public new AudioSource audio;
    public AudioClip powerUpClip;
    public AudioClip powerDownClip;
    public AudioClip starPowerClip;
    public AudioClip deathClip;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
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

        if (BackgroundMusic.Instance != null) {
            BackgroundMusic.Instance.StopMusic();
        }
        StartCoroutine(PlaySoundAndDisableMusic(powerDownClip));
        StartCoroutine(ScaleAnimation());
    }
    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;

        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        if (BackgroundMusic.Instance != null) {
            BackgroundMusic.Instance.StopMusic();
        }
        StartCoroutine(PlaySoundAndDisableMusic(powerUpClip));
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

        if (BackgroundMusic.Instance != null) {
            BackgroundMusic.Instance.StopMusic();
        }
        StartCoroutine(PlaySoundAndDisableMusic(deathClip));
        GameManager.Instance.ResetLevel(4f);   
    }
    public void StarPower(float duration = 12.5f)
    {
        starpower = true;

        if (BackgroundMusic.Instance != null) {
            BackgroundMusic.Instance.StopMusic();
        }
        StartCoroutine(PlaySoundAndDisableMusic(starPowerClip));
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
    private IEnumerator PlaySoundAndDisableMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip);

        yield return new WaitForSeconds(clip.length);

        if (BackgroundMusic.Instance != null){
            BackgroundMusic.Instance.PlayMusic(MusicType.Background);
        }
    }
}
