using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement: MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private new Camera camera;
    public float speed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => 2f * maxJumpHeight / (maxJumpTime / 2f);
    public float gravity => -2f * maxJumpHeight / Mathf.Pow(maxJumpTime / 2f, 2);
    public bool grounded {
        get; private set;
        }
    public bool jumping{
        get; private set;
    }
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    private Vector2 velocity;
    private float inputAxis;
    public new AudioSource audio;
    public AudioClip jumpSound;

    public void Awake()
    {
        camera = Camera.main; 
        rigidbody = GetComponent<Rigidbody2D>();  
        collider = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }
    private void OnDisable()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    public void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.RayCast(Vector2.down);

        if (grounded){
            GroundedMovement();
        }
        
        ApplyGravity();

    }
    public void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        float playerWidth = GetComponent<CapsuleCollider2D>().size.x / 2f;
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        position.x = Mathf.Clamp(position.x, leftEdge.x + playerWidth, rightEdge.x - playerWidth);

        rigidbody.MovePosition(position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            if (transform.DotTest(collision.transform, Vector2.down)){
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")){
            if (transform.DotTest(collision.transform, Vector2.up)){
                velocity.y = 0f;
            }
        }
    }
    public float friction = 2f;
    public void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * friction * Time.deltaTime);

        if (rigidbody.RayCast(Vector2.right * velocity.x)){
            velocity.x = 0f;
        }

        if (velocity.x > 0f){
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    public void GroundedMovement(){
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump")){
            velocity.y = jumpForce;
            jumping = true;
            audio.clip = jumpSound;
            audio.Play();
        }
    }
    public void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }
}
