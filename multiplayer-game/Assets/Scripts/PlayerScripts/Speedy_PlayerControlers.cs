using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Speedy_PlayerControlers : MonoBehaviour
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

    [Header("Sprint")]
    public float sprintMultiplier = 1.6f; // How much faster while sprinting
    private bool isSprinting = false;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.2f;
    public LayerMask wallLayer;
    public float wallJumpForce = 12f;
    public float wallJumpPush = 10f;

    private bool isWallSliding;
    public float wallSlideSpeed = -2f;
   // private DoorTeleportation doorTeleportation;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerFlipScript = GetComponent<FlipPlayer>();
    }

    // private void Start()
    // {
    //     doorTeleportation = FindFirstObjectByType<DoorTeleportation>();
    // }

    // Update is called once per frame
    void Update()
    {
        // if (doorTeleportation.gameIsNotReady == false && doorTeleportation.speedySpawned == false)
        // {
            MovementLogic();
            WallDetectionLogic();
        //}
    }

    private void MovementLogic()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer); //Returns true if the player overlaps with ground (Can Jump)
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed; // Apply if player is Sprinting
        rb.linearVelocity = new Vector2(movement.x * currentSpeed, rb.linearVelocity.y);

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime; //Resets Coyote if Grounded
        else
            coyoteCounter -= Time.deltaTime; //Depleat Coyote after leaving the ground

        // Jump buffer ticking down
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime; //Depleat Buffer after Jumping

        // WALL JUMP
        if (isWallSliding && jumpBufferCounter > 0)
        {
            // Push away from the wall
            float direction = -transform.localScale.x;

            rb.linearVelocity = new Vector2(direction * wallJumpPush, wallJumpForce);

            // Reset jump stuff
            jumpBufferCounter = 0;
            coyoteCounter = 0;

            return; // prevent normal jump and gravity logic from triggering
        }

        // Jump logic
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //Move Player X (Jump)
            coyoteCounter = 0;
            jumpBufferCounter = 0;
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

    void WallDetectionLogic()
    {
        bool isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, wallLayer);

        // Wall slide detection
        isWallSliding = isTouchingWall && !isGrounded && rb.linearVelocity.y < 0;

        // Wall slide
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, wallSlideSpeed));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        playerFlipScript.Flip(movement.x);
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
            isSprinting = true;

        if (context.canceled)
            isSprinting = false;
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
