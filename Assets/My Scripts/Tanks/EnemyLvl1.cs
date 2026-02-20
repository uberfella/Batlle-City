using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static Enemy;

public class EnemyLvl1 : Enemy  
{
    public static event Action<Enemy> OnDestroyed;

    public float timePassedSinceBlocked = 0f;
    public Sprite[] trackSprites;
    public Sprite[] trackSpritesPowerup;
    public float animationSpeed = 0.2f;
    public SpriteRenderer spriteRenderer;

    private readonly float changeDirectionTime = 0.25f; // Change direction every x milliseconds 
    private float timerForShooting;
    private int shotCooldown = 1;
    private bool requestNewCooldown = true;
    private bool requestNewDirection = true;
    private Vector2 currentMoveDirection = Vector2.zero;
    private int currentFrame = 0;
    private float timerForSpritesRender = 0f;
    private int lastUsedDirection = 0;
    private float timerForRandomDirection = 0f;
    private float changeRandomDirectionTime = 0f;
    private bool requestNewRandomVal = true;


    //neccessary for the score counting
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    void Start()
    {
        health = 1;
        speed = 2.5f;
        scoreOnDestroy = 100;
        projectileSpeed = 10f;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyType = EnemyType.EnemyLvl1;
        currentMoveDirection = getDirection();
        enemyIsAlive = true;
        //hasPowerup = true;
        //Debug.Log("hasPowerup = " + hasPowerup);
    }

    void Update()
    {
        if (isFrozen) return;
        //if (GameLogic.Instance.isEnemiesFrozen)
        //    return;
        //---------------
        //MOVING
        if (horizontalInput != 0 || verticalInput != 0)
        {
            timerForSpritesRender += Time.deltaTime;
            if (timerForSpritesRender >= animationSpeed)
            {
                timerForSpritesRender = 0f;
                if (!hasPowerup)
                {
                    currentFrame = (currentFrame + 1) % trackSprites.Length;
                    spriteRenderer.sprite = trackSprites[currentFrame];
                }
                else
                {
                    currentFrame = (currentFrame + 1) % trackSpritesPowerup.Length;
                    spriteRenderer.sprite = trackSpritesPowerup[currentFrame];
                }
            }
        }

        EnemyMove(currentMoveDirection);

        //Debug.Log("timePassedSinceBlocked = " + timePassedSinceBlocked);

        if (objectIsCurrentlyBeingBlocked)
        {
            timePassedSinceBlocked += Time.deltaTime;
            //Debug.Log("timePassedSinceBlocked: " + timePassedSinceBlocked);
            requestNewDirection = true;

            changeRandomDirectionTime = UnityEngine.Random.Range(1, 10);
            Debug.Log("setting changeRandomDirectionTime to " + changeRandomDirectionTime);


            timerForRandomDirection = 0f;
        }
        else
        {
            timePassedSinceBlocked = 0;

            timerForRandomDirection += Time.deltaTime;
            if(timerForRandomDirection >= changeRandomDirectionTime)
            {
                Debug.Log("requestin random new direction");
                SetMoveDirection(getDirection());
            }
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
            ShootTheGun();
            requestNewCooldown = true;
        }
        //-------------
    }

    private Vector2 getDirection()
    {
        //-1, 1, -1, 1
        //1,0 -1,0 0,1 0,-1
        //0    1   2   3

        switch (aiController.GetHorizontalVerticalInput())
        {
            case 0:
                horizontalInput = 1;
                verticalInput = 0;
                lastUsedDirection = 0;
                break;
            case 1:
                horizontalInput = -1;
                verticalInput = 0;
                lastUsedDirection = 1;
                break;
            case 2:
                horizontalInput = 0;
                verticalInput = 1;
                lastUsedDirection = 2;
                break;
            case 3:
                horizontalInput = 0;
                verticalInput = -1;
                lastUsedDirection = 3;
                break;
        }


        return new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void SetMoveDirection(Vector2 newDirection)
    {
        currentMoveDirection = newDirection;
    }

    //if (gameObject.CompareTag("enemy0"))
    //enemyIsAlive
    private int GetEnemyIndex(string tag) 
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        switch (tag) 
        {
            case "enemy0":
                return 0;
        }
        return 1;
    }

    //public override void TakeDamage(int amount)
    //{
    //    ChangeEnemyStatus();

    //    base.TakeDamage(amount);
    //}

    //public void ChangeEnemyStatus()
    //{
    //    switch (gameObject.layer)
    //    {
    //        case 7:
    //            //Debug.Log("7 is false");
    //            spawner.enemyAlive[0] = false;
    //            break;
    //        case 10:
    //            //Debug.Log("10 is false");
    //            spawner.enemyAlive[1] = false;
    //            break;
    //        case 11:
    //            //Debug.Log("11 is false");
    //            spawner.enemyAlive[2] = false;
    //            break;
    //        case 12:
    //            //Debug.Log("12 is false");
    //            spawner.enemyAlive[3] = false;
    //            break;
    //    }
    //}

    public int GetEnemyLayer() 
    {
        switch (gameObject.layer)
        {
            case 7:
                return 7;
            case 10:
                return 10;
            case 11:
                return 11;
            case 12:
                return 12;
        }
        return 0;
    }

}
