using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using static Enemy;

public class EnemyLvl1 : Enemy  
{
    public static event Action<Enemy> OnDestroyed;

    public float timePassedSinceBlocked = 0f;
    public Sprite[] tankSpritesFacingUp;
    public Sprite[] tankSpritesFacingLeft;
    public Sprite[] tankSpritesFacingRight;
    public Sprite[] tankSpritesFacingDown;
    public Sprite[] tankSpritesFacingUpPowerup;
    public Sprite[] tankSpritesFacingLeftPowerup;
    public Sprite[] tankSpritesFacingRightPowerup;
    public Sprite[] tankSpritesFacingDownPowerup;
    public float animationSpeed = 0.2f;
    public SpriteRenderer spriteRenderer;

    private readonly float changeDirectionTime = 0.25f; // Change direction every x milliseconds
    private float shotCooldown = 1f;
    private Vector2 currentMoveDirection = Vector2.zero;
    private Vector2 lastMoveDirection = Vector2.zero;
    private int currentFrame = 0;
    private float timerForSpritesRender = 0f;
    private float timerForRandomDirection = 0f;
    private float changeRandomDirectionTime = 0f;
    private bool cooldownForShootingHasPassed = true;

    private Sprite[] GetArray(float x, float y, bool hasPowerup) 
    {
        if (!hasPowerup)
        {
            if (x == 0)
            {
                if (y == 1)
                {
                    //Debug.Log("sprite up");
                    return tankSpritesFacingUp;
                }
                else
                {
                    //Debug.Log("sprite down");
                    return tankSpritesFacingDown;
                }
            }
            else if (x == -1)
            {
                //Debug.Log("sprite left");
                return tankSpritesFacingLeft;
            }
            else
            {
                //Debug.Log("sprite right");
                return tankSpritesFacingRight;
            }
        }
        else
        {
            if (x == 0)
            {
                if (y == 1)
                {
                    //Debug.Log("sprite up");
                    return tankSpritesFacingUpPowerup;
                }
                else
                {
                    //Debug.Log("sprite down");
                    return tankSpritesFacingDownPowerup;
                }
            }
            else if (x == -1)
            {
                //Debug.Log("sprite left");
                return tankSpritesFacingLeftPowerup;
            }
            else
            {
                //Debug.Log("sprite right");
                return tankSpritesFacingRightPowerup;
            }
        }

    }

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
        projectileSpeed = 8f;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyType = EnemyType.EnemyLvl1;
        currentMoveDirection = getDirection();
        enemyIsAlive = true;

        changeRandomDirectionTime = UnityEngine.Random.Range(1, 10);
    }

    void Update()
    {
        if (isFrozen) return;

        if (currentMoveDirection != lastMoveDirection)
        {
            lastMoveDirection = currentMoveDirection;

            currentFrame = 0;        // reset animation frame
            UpdateSpriteImmediate(); // update sprite instantly
        }

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
                    currentFrame = (currentFrame + 1) % GetArray(currentMoveDirection.x, currentMoveDirection.y, false).Length;
                    spriteRenderer.sprite = GetArray(currentMoveDirection.x, currentMoveDirection.y, false)[currentFrame];
                }
                else
                {
                    currentFrame = (currentFrame + 1) % GetArray(currentMoveDirection.x, currentMoveDirection.y, true).Length;
                    spriteRenderer.sprite = GetArray(currentMoveDirection.x, currentMoveDirection.y, true)[currentFrame];
                }
            }
        }

        EnemyMove(currentMoveDirection);

        timerForRandomDirection += Time.deltaTime;

        if (objectIsCurrentlyBeingBlocked)
        {
            timePassedSinceBlocked += Time.deltaTime;
        }
        else
        {
            timePassedSinceBlocked = 0;
        }

        if ((timePassedSinceBlocked >= changeDirectionTime) || (timerForRandomDirection >= changeRandomDirectionTime))
        {
            timePassedSinceBlocked = 0f;
            timerForRandomDirection = 0f;

            SetMoveDirection(getDirection());

            changeRandomDirectionTime = UnityEngine.Random.Range(1, 10);
        }
        //---------------

        //---------------
        //SHOOTING
        //getting new value with each call after each shot
        if (cooldownForShootingHasPassed)
        {
            ShootTheGun();
            cooldownForShootingHasPassed = false;
            TriggerShootCooldown();
        }
        //-------------
    }
    public void TriggerShootCooldown()
    {
        StartCoroutine(SetShootingCooldownCoroutine());
    }
    private IEnumerator SetShootingCooldownCoroutine()
    {
        yield return new WaitForSeconds(shotCooldown = aiController.GetShootCooldown());

        cooldownForShootingHasPassed = true;
    }

    private Vector2 getDirection()
    {
        switch (aiController.GetHorizontalVerticalInput())
        {
            case 0:
                horizontalInput = 1;
                verticalInput = 0;
                break;
            case 1:
                horizontalInput = -1;
                verticalInput = 0;
                //Debug.Log("go left");

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
        //Debug.Log("currentMoveDirection.x = " + currentMoveDirection.x);
        //Debug.Log("currentMoveDirection.y = " + currentMoveDirection.y);

    }

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

    void UpdateSpriteImmediate()
    {
        if (!hasPowerup)
        {
            spriteRenderer.sprite = GetArray(currentMoveDirection.x, currentMoveDirection.y, false)[currentFrame];
        }
        else
        {
            spriteRenderer.sprite = GetArray(currentMoveDirection.x, currentMoveDirection.y, true)[currentFrame];
        }
    }

}
