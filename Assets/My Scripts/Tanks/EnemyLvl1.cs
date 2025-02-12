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
            EnemyMove(aiController.GetVerticalRandom(), aiController.GetHorizontalRandom());
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

        ConstrainMovements();
    }

    
}
