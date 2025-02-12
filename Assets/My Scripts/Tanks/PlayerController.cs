using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

//The logic in this script has been copied from learn.unity.com lesson project with adjustments

public class PlayerController2D : Tank
{
    //public LayerMask obstacleLayer;
    public GameObject shellPrefab;

    //private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    //private BoxCollider2D boxCollider;
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally
    private float timerForShooting;
    private float shootCooldown = 1f;
    private bool cooldownHasPassed = true;


    void Start()
    {

        health = 1;
        speed = 5f;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        //PlayerMove();

        if (Input.GetKeyDown(KeyCode.Space) && cooldownHasPassed)
        {
            ShootTheGun();
            cooldownHasPassed = false;
        }

        timerForShooting += Time.deltaTime;
        if (timerForShooting >= shootCooldown)
        {
            timerForShooting = 0;
            cooldownHasPassed = true;
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        //rb.linearVelocity = movement * speed;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        RestrictDiagonalMovements();

        //if (verticalInput != 0)
        //{
        //    horizontalInput = 0;
        //}
        //else if (horizontalInput != 0)
        //{
        //    verticalInput = 0;
        //}

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (moveDirection != Vector2.zero)
        {
            PlayerMove(moveDirection);
        }

        //ConstrainMovements();
    }

    //private void RotatePlayer(float x, float y)
    //{
    //    // If there is no input, do not rotate the player
    //    if (x == 0 && y == 0) return;

    //    // Calculate the rotation angle based on input direction
    //    float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

    //    // Apply the rotation to the player
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //}

    //private void PlayerMove(Vector2 moveDir)
    //{

    //    Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;
    //    //Debug.Log("targetPosition = " + targetPosition);    

    //    // Use BoxCast instead of Raycast for better accuracy
    //    if (!IsBlocked(targetPosition, moveDir) && MovementIsWithinLevelsRange(targetPosition))
    //    {
    //        rb.MovePosition(targetPosition);
    //    }

    //    //{
    //    //    // Determine the priority of movement based on input
    //    if (horizontalInput != 0)
    //    {
    //        isMovingHorizontally = true;
    //    }
    //    else if (verticalInput != 0)
    //    {
    //        isMovingHorizontally = false;
    //    }

    //    // Set movement direction and optionally rotate the player
    //    if (isMovingHorizontally)
    //    {
    //        movement = new Vector2(horizontalInput, 0);


    //        //make the tank sprite face left or right depending on direction 
    //        if (horizontalInput == 1)
    //        {
    //            RotatePlayer(horizontalInput, -90);
    //        }
    //        else if (horizontalInput == -1)
    //        {
    //            RotatePlayer(horizontalInput, 90);
    //        }
    //    }
    //    else
    //    {
    //        movement = new Vector2(0, verticalInput);

    //        //make the tank sprite face up or down depending on direction 
    //        if (verticalInput == 1)
    //        {
    //            RotatePlayer(90, verticalInput);
    //        }
    //        else if (verticalInput == -1)
    //        {
    //            RotatePlayer(-90, verticalInput);
    //        }

    //    }
    //}


    //private bool IsBlocked(Vector2 targetPos, Vector2 moveDir)
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

    //    if (hit.collider != null)
    //    {
    //        //Debug.Log("Blocked by: " + hit.collider.gameObject.name);
    //        return true;
    //    }
    //    return false;
    //}

    private void ShootTheGun()
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }
}
