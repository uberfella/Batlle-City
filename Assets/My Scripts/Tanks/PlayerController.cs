using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

//The logic in this script has been copied from learn.unity.com lesson project with adjustments

public class PlayerController2D : Tank
{
    public LayerMask obstacleLayer;
    public GameObject shellPrefab;
    
    public bool playerIsAlive;

    private float timerForShooting;
    private float shootCooldown = 1f;
    private bool cooldownHasPassed = true;
    private PlayerSpawner playerSpawner;

    void Start()
    {

        health = 1;
        speed = 5f;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //playerSpawner = GetComponent<PlayerSpawner>(); hi
        playerIsAlive = true;
    }

    void Update()
    {

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

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        RestrictDiagonalMovements();

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        //Debug.Log("horizontalInput = " + horizontalInput);
        //Debug.Log("verticalInput = " + verticalInput);

        //if (moveDirection != Vector2.zero)
        {
            PlayerMove(moveDirection);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            playerIsAlive = false;
            PlayerSpawner.playerLives--;
            Debug.Log("playerLives = " + PlayerSpawner.playerLives);
        }
    }

    private void PlayerMove(Vector2 moveDir)
    {

        Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;

        if (!IsBlocked(targetPosition, moveDir))
        {
            rb.MovePosition(targetPosition);
        }

        if (horizontalInput == 1)
        {
            RotatePlayer(horizontalInput, -90);
        }
        else if (horizontalInput == -1)
        {
            RotatePlayer(horizontalInput, 90);
        }

        if (verticalInput == 1)
        {
            RotatePlayer(90, verticalInput);
        }
        else if (verticalInput == -1)
        {
            RotatePlayer(-90, verticalInput);
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
            //Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

    private void ShootTheGun()
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }
}
