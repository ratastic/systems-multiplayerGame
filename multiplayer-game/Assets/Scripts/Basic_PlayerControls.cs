using UnityEngine;
using UnityEngine.InputSystem;

public class Basic_PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private Vector2 movement;
    private Rigidbody2D rb;

    private static int PlayerNumber = 1;

    [Header("Jump")]
    public float jumpForce = 10f;
    public float coyoteTime = 0.15f; // Coyote Time: Gives player extra time to jump after leaving the ground
    private float coyoteCounter;

    public float jumpBufferTime = 0.15f; // Jump before landing (Player Can Spam Jump)
    private float jumpBufferCounter;

    public Transform groundCheck;
    private bool isGrounded;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer; //Using a layermask, we can use collision script later as well

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Log Player Number
        Debug.Log($"hi i am player {PlayerNumber}");
        PlayerNumber++;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer); //Returns true if the player overlaps with ground (Can Jump)
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y); //Move Player Y (Move)

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Jump buffer ticking down
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime;

        // Jump logic
        if (jumpBufferCounter > 0 && coyoteCounter > 0 )
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
            jumpBufferCounter = jumpBufferTime;
        }

    }
}