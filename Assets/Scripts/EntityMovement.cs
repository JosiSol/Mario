using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    public Vector2 direction = Vector2.left;
    public float speed = 1f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;   
    }
    private void OnEnable()
    {
       rigidbody.WakeUp();
    }
    private void OnDisable()
    {
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.Sleep();
    }
    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (rigidbody.RayCast(direction)) 
        {
            direction = -direction;
        }
        if (rigidbody.RayCast(Vector2.down))
        {
            velocity.y = Mathf.Max(0, velocity.y);
        }
    }
}


