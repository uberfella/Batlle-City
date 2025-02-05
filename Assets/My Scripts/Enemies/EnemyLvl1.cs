using UnityEngine;

public class EnemyLvl1 : Enemy
{
    public GameObject shellPrefab;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    void Start()
    {
        health = 1;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.linearVelocity = movement * speed;
    }

    void ShootTheGun()
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }
    public void RotateEnemy(float x, float y)
    {
        // If there is no input, do not rotate the player
        if (x == 0 && y == 0) return;

        // Calculate the rotation angle based on input direction
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void EnemyMove()
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
                    RotateEnemy(horizontalInput, -90);
                }
                else if (horizontalInput == -1)
                {
                    RotateEnemy(horizontalInput, 90);
                }
            }
            else
            {
                movement = new Vector2(0, verticalInput);

                //make the tank sprite face up or down depending on direction 
                if (verticalInput == 1)
                {
                    RotateEnemy(90, verticalInput);
                }
                else if (verticalInput == -1)
                {
                    RotateEnemy(-90, verticalInput);
                }

            }
        }
    }
}
