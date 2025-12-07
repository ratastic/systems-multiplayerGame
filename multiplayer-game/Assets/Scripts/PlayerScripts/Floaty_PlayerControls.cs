using UnityEngine;
using UnityEngine.InputSystem;

public class Floaty_PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private Vector2 movement;
    private Rigidbody2D rb;
    private FlipPlayer playerFlipScript;

    [Header("Jump")]
    public float jumpForce = 10f;

    public float fallGravity = 2f;        // Faster fall
    public float lowJumpGravity = 4f;     // More gravity when jump is released early
    private bool jumpHeld;

    public float coyoteTime = 0.15f; // Coyote Time: Player can jump after leaving the ground (Forgiving to late jump inputs)
    private float coyoteCounter;

    public float jumpBufferTime = 0.15f; // Buffer: Player can jump after fully landing (Forgiving to early jump inputs)
    private float jumpBufferCounter;

    public Transform groundCheck;
    private bool isGrounded;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer; //Using a layermask, better than collision bs

    [Header("Double Jump")]
    public int maxJumps = 2;   // How many total jumps
    private int jumpCount;     // How many jumps used

    [Header("Dash")]
    public float dashForce = 20f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.3f;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerFlipScript = GetComponent<FlipPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementLogic();
        DashLogic();
    }

    private void MovementLogic()
    {
        if (isDashing) return; // movement disabled while dashing

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer); //Returns true if the player overlaps with ground (Can Jump)
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y); //Move Player Y (Move)

        // Coyote time
        if (isGrounded)
        {
            coyoteCounter = coyoteTime; //Resets Coyote if Grounded
            jumpCount = 0;   // Reset jumps on landing
        }
        else
            coyoteCounter -= Time.deltaTime; //Depleat Coyote after leaving the ground

        // Jump buffer ticking down
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime; //Depleat Buffer after Jumping

        // Jump logic
        if (jumpBufferCounter > 0 && (coyoteCounter > 0 || jumpCount < maxJumps))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //Move Player X (Jump)

            jumpBufferCounter = 0;
            coyoteCounter = 0;

            jumpCount++; // Add to Count Jump
        }

        // Better Gravity
        if (rb.linearVelocity.y < 0) // Falling
        {
            rb.gravityScale = fallGravity;
        }
        else if (rb.linearVelocity.y > 0 && !jumpHeld) // Jump let go early
        {
            rb.gravityScale = lowJumpGravity;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
    private void DashLogic()
    {
        // Cooldown Timer
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // If player is currently dashing
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.5f, rb.linearVelocity.y); // Stop extra velocity
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (dashCooldownTimer > 0) return; //If player cooldown is still active, return
        if (isDashing) return; //If player is already dashing, return

        //Reset Timers
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        // If no movement input, dash in previous facing direction
        Vector2 dashDir = movement != Vector2.zero ? movement.normalized : new Vector2(transform.localScale.x, 0);

        rb.linearVelocity = dashDir * dashForce; //Apply Dash
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        playerFlipScript.Flip(movement.x);

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpHeld = true;
            jumpBufferCounter = jumpBufferTime; //Resets Buffer if Jump
        }

        if (context.canceled)
        {
            jumpHeld = false;
        }

    }
}
