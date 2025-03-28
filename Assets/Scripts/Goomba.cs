using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private bool isFlattened;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFlattened) return;
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.dead) return;
            if (player.starpower){
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)){
                Flatten();
            }
            else {
                player.Hit();
            }
        }
    }
    private void Flatten()
    {
        isFlattened = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false; 
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
