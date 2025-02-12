using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public LayerMask obstacleLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection != Vector2.zero)
        {
            Move(moveDirection);
        }
    }

    private void Move(Vector2 moveDir)
    {
        Vector2 targetPosition = rb.position + moveDir * moveSpeed * Time.fixedDeltaTime;

        // Use BoxCast instead of Raycast for better accuracy
        if (!IsBlocked(targetPosition, moveDir))
        {
            rb.MovePosition(targetPosition);
        }
    }

    private bool IsBlocked(Vector2 targetPos, Vector2 moveDir)
    {
        // Cast a box to detect collisions ahead
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,  // Cast from collider center
            boxCollider.bounds.size,    // Use actual collider size
            0f,                         // No rotation
            moveDir,                    // Move direction
            0.1f,                        // Distance to check
            obstacleLayer                // Check against obstacles
        );

        if (hit.collider != null)
        {
            Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }
}
