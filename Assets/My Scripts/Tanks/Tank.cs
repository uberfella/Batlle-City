using UnityEngine;

public class Tank : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected int health;
    protected float speed;
    protected Vector2 minBounds = new Vector2(-5.5f, -5.5f); // -6 -6 7 7
    protected Vector2 maxBounds = new Vector2(6.5f, 6.5f);


    protected virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void ConstrainMovements()
    {
        // Clamp position to keep object inside the boundaries
        float clampedX = Mathf.Clamp(rb.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(rb.position.y, minBounds.y, maxBounds.y);

        // Apply clamped position to Rigidbody2D
        rb.position = new Vector2(clampedX, clampedY);

        // If the position was clamped, prevent further movement in that direction
        if (rb.position.x != clampedX)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Stop horizontal movement

        if (rb.position.y != clampedY)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Stop vertical movement
    }


}
