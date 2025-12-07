using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    private bool facingRight = true;

    public void Flip(float moveInput)
    {
        // Do nothing if not moving
        if (moveInput == 0) return;

        // Flip left
        if (moveInput < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Flip right
        else if (moveInput > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public bool IsFacingRight() => facingRight;
}

