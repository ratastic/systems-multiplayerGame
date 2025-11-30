using UnityEngine;
using UnityEngine.InputSystem;

public class Floaty_PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private Vector2 movement;
    private Rigidbody2D rb;

    [Header("Jump")]
    public float jumpForce = 10f;
    public float coyoteTime = 0.15f; // Coyote Time: Player can jump after leaving the ground (Forgiving to late jump inputs)
    private float coyoteCounter;

    public float jumpBufferTime = 0.15f; // Buffer: Player can jump after fully landing (Forgiving to early jump inputs)
    private float jumpBufferCounter;

    public Transform groundCheck;
    private bool isGrounded;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer; //Using a layermask, better than collision bs

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer); //Returns true if the player overlaps with ground (Can Jump)
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y); //Move Player Y (Move)

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime; //Resets Coyote if Grounded
        else
            coyoteCounter -= Time.deltaTime; //Depleat Coyote after leaving the ground

        // Jump buffer ticking down
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime; //Depleat Buffer after Jumping

        // Jump logic
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //Move Player X (Jump)
            coyoteCounter = 0;
            jumpBufferCounter = 0;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferCounter = jumpBufferTime; //Resets Buffer if Jump
        }

    }
}
