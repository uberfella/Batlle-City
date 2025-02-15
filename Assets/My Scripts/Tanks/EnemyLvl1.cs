using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyLvl1 : Enemy
{

    private readonly float changeDirectionTime = 2f; // Change direction every 2 seconds 
    private float timerForShooting;
    private int shotCooldown = 1;
    private float timerForDirection = 1.8f;
    private bool requestNewCooldown = true;
    private bool requestNewDirection = true;

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

        //Vector2 moveDirection = new Vector2(aiController.GetHorizontalRandom(), aiController.GetVerticalRandom()).normalized;
        //Debug.Log("requesting ai values horizontal of which = " + aiController.GetHorizontalRandom());
        //EnemyMove(moveDirection);




        //EnemyMove(aiController.GetVerticalRandom(), aiController.GetHorizontalRandom());
        //Vector2 moveDirection = new Vector2(aiController.GetHorizontalRandom(), aiController.GetVerticalRandom()).normalized;
        //Debug.Log("requesting ai values horizontal of which = " + aiController.GetHorizontalRandom());

        //EnemyMove(moveDirection);

        timerForDirection += Time.deltaTime;
        if (timerForDirection >= changeDirectionTime)
        {
            timerForDirection = 0;
            Vector2 moveDirection = new Vector2(aiController.GetHorizontalRandom(), aiController.GetVerticalRandom()).normalized;
            Debug.Log("requesting ai values horizontal of which = " + aiController.GetHorizontalRandom());
            EnemyMove(moveDirection);
            //requestNewDirection = true;
        }


        //that works w/o stutters
        //Vector2 moveDirection = new Vector2(1, 1);
        //EnemyMove(moveDirection);

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


        //ConstrainMovements();

    }

    //public void EnemyMove(Vector2 moveDir)
    //{
    //    //possible values for both inputs are -1, 0, 1
    //    Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;

    //    if (!IsBlocked(targetPosition, moveDir) && MovementIsWithinLevelsRange(targetPosition))
    //    {
    //        rb.MovePosition(targetPosition);
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

}
