using UnityEngine;
using UnityEngine.InputSystem;

public class Basic_PlayerControls : MonoBehaviour
{
    public float speed;
    private Vector2 movement;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}