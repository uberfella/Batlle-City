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
        //1,0 -1,0 0,1 0,-1
        //0    1   2   3

        //this takes a toll on CPU no cap
        //do
        //{
        //    horizontalInput = aiController.GetHorizontalRandom();
        //    verticalInput = aiController.GetVerticalRandom();
        //}
        //while (horizontalInput == verticalInput);

        switch (aiController.GetHorizontalVerticalInput())
        {
            case 0:
                horizontalInput = 1;
                verticalInput = 0;
                break;
            case 1:
                horizontalInput = -1;
                verticalInput = 0;
                break;
            case 2:
                horizontalInput = 0;
                verticalInput = 1;
                break;
            case 3:
                horizontalInput = 0;
                verticalInput = -1;
                break;
        }


        return new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void SetMoveDirection(Vector2 newDirection)
    {
        currentMoveDirection = newDirection;
    }

    protected void EnemyMove(Vector2 moveDir)
    {
        //possible values for both inputs are -1, 0, 1
        //Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;
        Vector2 targetPosition = (Vector2)transform.position + moveDir * speed * Time.deltaTime;

        //Debug.Log("horizontalInput = " + horizontalInput);
        //Debug.Log("verticalInput = " + verticalInput);  

        if (!IsBlocked(targetPosition, moveDir))
        {
            //rb.MovePosition(targetPosition);
            transform.position = targetPosition;
        }

        if (IsBlocked(targetPosition, moveDir))
        {
            objectIsCurrentlyBeingBlocked = true;
        }
        else
        {
            objectIsCurrentlyBeingBlocked = false;
        }

        //make the tank sprite face left or right depending on direction 
        if (horizontalInput == 1)
        {
            RotatePlayer(horizontalInput, -90);
        }
        else if (horizontalInput == -1)
        {
            RotatePlayer(horizontalInput, 90);
        }


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

        if (/*hit.collider != null && */hit.collider.gameObject != gameObject)
        {

            Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

}
