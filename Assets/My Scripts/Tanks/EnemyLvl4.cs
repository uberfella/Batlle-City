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

    private readonly float changeDirectionTime = 0.25f; // Change direction every x milliseconds
    private int shotCooldown = 1;
    private Vector2 currentMoveDirection = Vector2.zero;
    private int currentFrame = 0;
    private float timerForSpritesRender = 0f;
    private float timerForRandomDirection = 0f;
    private float changeRandomDirectionTime = 0f;
    private bool cooldownForShootingHasPassed = true;

    void Start()
    {
        health = 4;
        speed = 2.5f;
        scoreOnDestroy = 400;
        projectileSpeed = 10f;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyType = EnemyType.EnemyLvl4;
        currentMoveDirection = getDirection();
        enemyIsAlive = true;
        changeRandomDirectionTime = UnityEngine.Random.Range(1, 10);
    }

    void Update()
    {
        if (isFrozen) return;
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

