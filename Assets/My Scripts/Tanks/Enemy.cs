using UnityEngine;

public class Enemy : Tank
{
    public GameObject shellPrefab;
    public int scoreOnDestroy;
    public Vector2 movement;
    public AiController aiController;
    public LayerMask obstacleLayer;
    protected bool objectIsCurrentlyBeingBlocked;

    protected void ShootTheGun()
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }

    //protected void EnemyMove(Vector2 moveDir)
    //{
    //    //possible values for both inputs are -1, 0, 1
    //    //Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;
    //    Vector2 targetPosition = (Vector2)transform.position + moveDir * speed * Time.deltaTime;

    //    //Debug.Log("horizontalInput = " + horizontalInput);
    //    //Debug.Log("verticalInput = " + verticalInput);  

    //    if (!IsBlocked(targetPosition, moveDir))
    //    {
    //        //rb.MovePosition(targetPosition);
    //        transform.position = targetPosition;
    //    }

    //    if (IsBlocked(targetPosition, moveDir))
    //    {
    //        objectIsCurrentlyBeingBlocked = true;
    //    }
    //    else 
    //    {
    //        objectIsCurrentlyBeingBlocked = false;
    //    }

    //    //make the tank sprite face left or right depending on direction 
    //    if (horizontalInput == 1)
    //    {
    //        RotatePlayer(horizontalInput, -90);
    //    }
    //    else if (horizontalInput == -1)
    //    {
    //        RotatePlayer(horizontalInput, 90);
    //    }


    //    //make the tank sprite face up or down depending on direction 
    //    if (verticalInput == 1)
    //    {
    //        RotatePlayer(90, verticalInput);
    //    }
    //    else if (verticalInput == -1)
    //    {
    //        RotatePlayer(-90, verticalInput);
    //    }

    //}

    //protected bool IsBlocked(Vector2 targetPos, Vector2 moveDir)
    //{
    //    // Cast a box to detect collisions ahead
    //    RaycastHit2D hit = Physics2D.BoxCast(
    //        boxCollider.bounds.center,  // Cast from collider center
    //        boxCollider.bounds.size,    // Use actual collider size
    //        0f,                         // No rotation
    //        moveDir,                    // Move direction
    //        0.1f,                        // Distance to check
    //        obstacleLayer                // Check against obstacles
    //    );

    //    if (hit.collider != null && hit.collider.gameObject != gameObject)
    //    {

    //        Debug.Log("Blocked by: " + hit.collider.gameObject.name);
    //        return true;
    //    }
    //    return false;
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == gameObject.layer)
    //    {
    //        // Stop movement or resolve collision manually
    //        transform.position -= transform.right * Time.deltaTime * speed;
    //    }
    //}

}
