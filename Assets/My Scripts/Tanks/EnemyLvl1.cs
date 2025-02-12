using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyLvl1 : Enemy
{

    private readonly float changeDirectionTime = 2f; // Change direction every 2 seconds 
    private float timerForShooting;
    private int shotCooldown = 1;
    private float timerForDirection = 1.8f;
    private bool requestNewCooldown = true;

    void Start()
    {
        health = 1;
        speed = 5f;
        scoreOnDestroy = 1;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //---------------
        //MOVING
        timerForDirection += Time.deltaTime;
        if (timerForDirection >= changeDirectionTime)
        {
            timerForDirection = 0;
            //EnemyMove(aiController.GetVerticalRandom(), aiController.GetHorizontalRandom());
            Vector2 moveDirection = new Vector2(aiController.GetHorizontalRandom(), aiController.GetVerticalRandom()).normalized;
            if (moveDirection != Vector2.zero)
            {
                //EnemyMove(moveDirection);
            }
                
        }
        //---------------

        //---------------
        //SHOOTING
        //getting new value with each call after each shot
        if (requestNewCooldown) 
        {
            shotCooldown = aiController.GetShootCooldown();
            requestNewCooldown = false;
        }
        timerForShooting += Time.deltaTime;
        if (timerForShooting >= shotCooldown)
        {
            timerForShooting = 0;
            ShootTheGun();
            requestNewCooldown = true;
        }
        //-------------
    }

    void FixedUpdate()
    {
        // Apply movement to the enemy in FixedUpdate for physics consistency
        rb.linearVelocity = movement * speed;

        //ConstrainMovements();
        
    }

    public void EnemyMove(Vector2 moveDir)
    {
        //possible values for both inputs are -1, 0, 1
        Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;

        if (!IsBlocked(targetPosition, moveDir) && MovementIsWithinLevelsRange(targetPosition))
        {
            rb.MovePosition(targetPosition);
        }

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
