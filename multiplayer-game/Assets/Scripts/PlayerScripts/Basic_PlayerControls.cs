using UnityEngine;
using UnityEngine.InputSystem;

public class Basic_PlayerControls : MonoBehaviour
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

    [Header("Interaction")]
    private bool canInteract1 = false;
    private bool canInteract2 = false;

    public PlayerInput playerInput;
    private GameManager gameManager;
    private PlayerInformation playerInfo;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        playerFlipScript = GetComponent<FlipPlayer>();
        playerInfo = GetComponent<PlayerInformation>();
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

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        playerFlipScript.Flip(movement.x);

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (canInteract1 == true)
        {
            SelectCricketCharacter(); //Select Floaty
        }
        else if (canInteract2 == true)
        {
            SelectFlyCharacter(); //Select Speedy
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1Selection")
        {
            canInteract1 = true;
            gameManager.TogglePlayerSelection1On(); //Active Label from other script
        }
        else if (other.tag == "Player2Selection")
        {
            canInteract2 = true;
            gameManager.TogglePlayerSelection2On(); //Active Label from other script
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1Selection")
        {
            canInteract1 = false;
            gameManager.TogglePlayerSelection1Off(); //Deactive Label from other script
        }
        else if (other.tag == "Player2Selection")
        {
            canInteract2 = false;
            gameManager.TogglePlayerSelection2Off(); //Deactive Label from other script
        }

    }
    private void SelectCricketCharacter()
    {
        gameManager.DeletePlayerSelectionObject1(); //Delete trigger

        playerInfo.PlayerAssignedCricket = true;
        playerInfo.ActivateUI();

        gameObject.GetComponent<Floaty_PlayerControls>().enabled = true; //Enable new Controls
        playerInput.SwitchCurrentActionMap("Floaty_Gameplay"); //Switch Action Inputs

        Speedy_PlayerControlers otherscript = GetComponent<Speedy_PlayerControlers>();
        Destroy(otherscript); //Destroy other script
        Destroy(this); //Destroy this script
    }
    private void SelectFlyCharacter()
    {
        gameManager.DeletePlayerSelectionObject2(); //Delete trigger

        playerInfo.PlayerAssignedFly = true;
        playerInfo.ActivateUI();

        gameObject.GetComponent<Speedy_PlayerControlers>().enabled = true; //Enable new Controls
        playerInput.SwitchCurrentActionMap("Speedy_Gameplay"); //Switch Action Inputs

        Floaty_PlayerControls otherscript = GetComponent<Floaty_PlayerControls>();
        Destroy(otherscript); //Destroy other script
        Destroy(this); //Destroy this script
    }

}