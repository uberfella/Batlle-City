using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyLvl1 : Enemy
{

    private readonly float changeDirectionTime = 0.5f; // Change direction every x milliseconds 
    private float timerForShooting;
    private int shotCooldown = 1;
    public float timePassedSinceBlocked = 0f;
    private bool requestNewCooldown = true;
    private bool requestNewDirection = true;
    private Vector2 currentMoveDirection = Vector2.zero;

    void Start()
    {
        health = 1;
        speed = 5f;
        scoreOnDestroy = 1;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentMoveDirection = getDirection();
    }



    // Update is called once per frame
    void Update()
    {
        //---------------
        //MOVING

        EnemyMove(currentMoveDirection);
        //Debug.Log("timePassedSinceBlocked = " + timePassedSinceBlocked);

        if (objectIsCurrentlyBeingBlocked)
        {
            timePassedSinceBlocked += Time.deltaTime;
            //Debug.Log("timePassedSinceBlocked: " + timePassedSinceBlocked);
            requestNewDirection = true;
        }
        else
        {
            //Debug
            timePassedSinceBlocked = 0;
        }

        if (requestNewDirection && timePassedSinceBlocked >= changeDirectionTime)
        {
            //Debug.Log("Requesting new direction");
            //Debug.Log("--------------");
            //Debug.Log("--------------");
            //Debug.Log("--------------");
            timePassedSinceBlocked = 0;
            //Vector2 moveDirection = new Vector2(aiController.GetHorizontalRandom(), aiController.GetVerticalRandom()).normalized;

            //
            SetMoveDirection(getDirection());
            //Debug.Log("getDirection() = " + getDirection());
            requestNewDirection = false;
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
            //ShootTheGun();
            requestNewCooldown = true;
        }
        //-------------
    }

    void FixedUpdate()
    {


    }

    private Vector2 getDirection()
    {
        //-1, 1, -1, 1
        //

        //this takes a toll on CPU no cap
        do
        {
            horizontalInput = aiController.GetHorizontalRandom();
            verticalInput = aiController.GetVerticalRandom();
        }
        while (horizontalInput == verticalInput);

        //does it work?
        RestrictDiagonalMovements();

        return new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void SetMoveDirection(Vector2 newDirection)
    {
        currentMoveDirection = newDirection;
    }


}
