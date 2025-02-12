using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

//The logic in this script has been copied from learn.unity.com with adjustments

public class PlayerController2D : Tank
{

    public GameObject shellPrefab;

    //private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally
    private float timerForShooting;
    private float shootCooldown = 1f;
    private bool cooldownHasPassed = true;

    void Start()
    {

        health = 1;
        speed = 5f;

        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        PlayerMove();

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
        rb.linearVelocity = movement * speed;

        ConstrainMovements();
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

    private void PlayerMove()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        {
            // Determine the priority of movement based on input
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
    }

    private void ShootTheGun() 
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }
}
