using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyLvl4 : Enemy
{
    public static event Action<Enemy> OnDestroyed;

    public float timePassedSinceBlocked = 0f;
    public Sprite[] trackSpritesHealth4;
    public Sprite[] trackSpritesHealth3;
    public Sprite[] trackSpritesHealth2;
    public Sprite[] trackSpritesHealth1;
    public Sprite[] trackSpritesHealth4Powerup;
    public float animationSpeed = 0.2f;
    public SpriteRenderer spriteRenderer;

    private readonly float changeDirectionTime = 0.5f; // Change direction every x milliseconds 
    private float timerForShooting;
    private int shotCooldown = 1;
    private bool requestNewCooldown = true;
    private bool requestNewDirection = true;
    private Vector2 currentMoveDirection = Vector2.zero;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        health = 4;
        //Debug.Log("health = " + health);
        speed = 2.5f;
        scoreOnDestroy = 400;
        projectileSpeed = 10f;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyType = EnemyType.EnemyLvl4;
        currentMoveDirection = getDirection();
        enemyIsAlive = true;
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
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0f;

                if (!hasPowerup)
                {
                    currentFrame = (currentFrame + 1) % GetSpriteColor().Length;
                    spriteRenderer.sprite = GetSpriteColor()[currentFrame];
                }
                else
                {
                    currentFrame = (currentFrame + 1) % GetSpriteColorWPowerup().Length;
                    spriteRenderer.sprite = GetSpriteColorWPowerup()[currentFrame];
                }
            }

            EnemyMove(currentMoveDirection);

            if (objectIsCurrentlyBeingBlocked)
            {
                timePassedSinceBlocked += Time.deltaTime;
                requestNewDirection = true;
            }
            else
            {
                timePassedSinceBlocked = 0;
            }

            if (requestNewDirection && timePassedSinceBlocked >= changeDirectionTime)
            {
                timePassedSinceBlocked = 0;

                SetMoveDirection(getDirection());
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
    //            Debug.Log("7 is false");
    //            spawner.enemyAlive[0] = false;
    //            break;
    //        case 10:
    //            Debug.Log("10 is false");
    //            spawner.enemyAlive[1] = false;
    //            break;
    //        case 11:
    //            Debug.Log("11 is false");
    //            spawner.enemyAlive[2] = false;
    //            break;
    //        case 12:
    //            Debug.Log("12 is false");
    //            spawner.enemyAlive[3] = false;
    //            break;
    //    }
    //}

    //0 1, 2 3, 4 5, 6 7
    //currentFrame = (currentFrame + 1) % trackSprites.Length;
    private Sprite[] GetSpriteColor() 
    {
        switch (health) 
        {
            case 1:
                return trackSpritesHealth1;
            case 2:
                return trackSpritesHealth2;
            case 3:
                return trackSpritesHealth3;
            case 4:
                return trackSpritesHealth4;
            default:
                return trackSpritesHealth4;
        }
    }

    private Sprite[] GetSpriteColorWPowerup()
    {
        switch (health)
        {
            case 4:
                return trackSpritesHealth4Powerup;
            default:
                return trackSpritesHealth4Powerup;
        }
    }

    //neccessary for the score counting
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

}

