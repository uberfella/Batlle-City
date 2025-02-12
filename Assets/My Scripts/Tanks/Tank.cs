using UnityEngine;

public class Tank : MonoBehaviour
{
    /// <summary>
    /// XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxxx
    /// separate layers for player and enemies
    /// </summary>
    public LayerMask obstacleLayer;
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected int health;
    protected float speed;
    protected Vector2 minBounds = new Vector2(-5.5f, -5.5f); // -6 -6 7 7
    protected Vector2 maxBounds = new Vector2(6.5f, 6.5f);
    protected float horizontalInput;
    protected float verticalInput;
    protected bool isMovingHorizontally = true;
    protected Vector2 movement;

    protected virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected bool MovementIsWithinLevelsRange(Vector2 targetPosition)
    {
        if (targetPosition.x > minBounds.x &&
            targetPosition.x < maxBounds.x &&
            targetPosition.y > minBounds.y &&
            targetPosition.y < maxBounds.y)
        {
            return true;
        }
        return false;
    }

    //so in the google play ripoff there is obviously no diagonal movement just like in the NES original
    //but in the said ripoff if you hold vertical you can not change the direction to the horizontal, and if you hold horizontal you can change it to the vertical immediately
    //I'm too lazy to check if that's the case in the original but anyways here's my take on that logic
    //let's declare it in the superclass so the AI classes can use it as well
    protected void RestrictDiagonalMovements() 
    {
        if (verticalInput != 0)
        {
            horizontalInput = 0;
        }
        else if (horizontalInput != 0)
        {
            verticalInput = 0;
        }
    }

    protected void PlayerMove(Vector2 moveDir)
    {

        Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;
        //Debug.Log("targetPosition = " + targetPosition);    

        // Use BoxCast instead of Raycast for better accuracy
        if (!IsBlocked(targetPosition, moveDir) && MovementIsWithinLevelsRange(targetPosition))
        {
            rb.MovePosition(targetPosition);
        }

        //{
        //    // Determine the priority of movement based on input
        if (horizontalInput != 0)
        {
            isMovingHorizontally = true;
        }
        else if (verticalInput != 0)
        {
            isMovingHorizontally = false;
        }

        // Set movement direction and optionally rotate the player
        if (isMovingHorizontally)
        {
            movement = new Vector2(horizontalInput, 0);


            //make the tank sprite face left or right depending on direction 
            if (horizontalInput == 1)
            {
                RotatePlayer(horizontalInput, -90);
            }
            else if (horizontalInput == -1)
            {
                RotatePlayer(horizontalInput, 90);
            }
        }
        else
        {
            movement = new Vector2(0, verticalInput);

            //make the tank sprite face up or down depending on direction 
            if (verticalInput == 1)
            {
                RotatePlayer(90, verticalInput);
            }
            else if (verticalInput == -1)
            {
                RotatePlayer(-90, verticalInput);
            }

        }
    }

    private void RotatePlayer(float x, float y)
    {
        // If there is no input, do not rotate the player
        if (x == 0 && y == 0) return;

        // Calculate the rotation angle based on input direction
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected bool IsBlocked(Vector2 targetPos, Vector2 moveDir)
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
            //Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

}
