using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MovementSettings settings;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumpRequested;
    private float moveInput;

    private float jumpVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        ApplySettings();
    }

    void ApplySettings()
    {
        rb.gravityScale = settings.gravityScale;

        jumpVelocity = Mathf.Sqrt(
            2f * Physics2D.gravity.magnitude *
            settings.gravityScale *
            settings.jumpHeight
        );
    }

    // Called by input or clone
    public void SetMoveInput(float input)
    {
        moveInput = Mathf.Clamp(input, -1f, 1f);
    }

    public void RequestJump()
    {
        
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveInput * settings.moveSpeed,
            rb.linearVelocity.y
        );

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            jumpRequested = false;
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}