using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyLvl4 : Enemy
{
    public float timePassedSinceBlocked = 0f;
    public Sprite[] trackSpritesHealth4;
    public Sprite[] trackSpritesHealth3;
    public Sprite[] trackSpritesHealth2;
    public Sprite[] trackSpritesHealth1;
    public float animationSpeed = 0.2f;
    public SpriteRenderer spriteRenderer;

    private readonly float changeDirectionTime = 0.5f; // Change direction every x milliseconds 
    private float timerForShooting;
    private int shotCooldown = 1;
    private bool requestNewCooldown = true;
    private bool requestNewDirection = true;
    private Vector2 currentMoveDirection = Vector2.zero;
    private Spawner spawner;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        health = 4;
        speed = 2.5f;
        scoreOnDestroy = 100;
        projectileSpeed = 10f;
        aiController = GetComponent<AiController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spawner = FindFirstObjectByType<Spawner>();
        currentMoveDirection = getDirection();
        enemyIsAlive = true;
    }
    //green, yellowish, green, white

    void Update()
    {
        Debug.Log("health = " + health);
        //---------------
        //MOVING
        if (horizontalInput != 0 || verticalInput != 0)
        {
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0f;
                
                currentFrame = (currentFrame + 1) % GetSpriteColor().Length;
                spriteRenderer.sprite = GetSpriteColor()[currentFrame];
            }
        }

        //EnemyMove(currentMoveDirection);

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
            //ShootTheGun();
            requestNewCooldown = true;
        }
        //-------------
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

    public override void TakeDamage(int amount)
    {
        ChangeEnemyStatus();

        base.TakeDamage(amount);
    }

    public void ChangeEnemyStatus()
    {
        switch (gameObject.layer)
        {
            case 7:
                Debug.Log("7 is false");
                spawner.enemyAlive[0] = false;
                break;
            case 10:
                Debug.Log("10 is false");
                spawner.enemyAlive[1] = false;
                break;
            case 11:
                Debug.Log("11 is false");
                spawner.enemyAlive[2] = false;
                break;
            case 12:
                Debug.Log("12 is false");
                spawner.enemyAlive[3] = false;
                break;
        }
    }

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

}

