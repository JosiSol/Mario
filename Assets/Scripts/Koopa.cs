using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    private bool shell;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shell && collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.dead) return;
            if (player.starpower){
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)){
                EnterShell();
            }
            else {
                player.Hit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shell && collision.gameObject.TryGetComponent(out Player player))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                if (player.starpower){
                    Hit();
                }
                else if (player != null)
                {
                    player.Hit();
                }
            }
        }
        else if (!shell && collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void EnterShell()
    {
        shell = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }
    private void PushShell(Vector2 direction){
        pushed = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        entityMovement.direction = direction.normalized;
        entityMovement.speed = 12f;
        entityMovement.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false; 
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }       
    }
}
