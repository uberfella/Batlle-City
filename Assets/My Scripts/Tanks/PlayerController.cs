using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

//The RotatePlayer logic in this script has been copied from learn.unity.com lesson project with adjustments

public class PlayerController2D : Tank
{
    public LayerMask obstacleLayer;
    public GameObject shellPrefab;
    public bool playerIsAlive;
    public bool playerIsInvincible;
    public Sprite[] tankLevelOneSprites;
    public Sprite[] tankLevelTwoSprites;
    public Sprite[] tankLevelThreeSprites;
    public Sprite[] tankLevelFourSprites;
    public float animationSpeed = 0.2f; // Time between frames
    public SpriteRenderer spriteRenderer;
    public int playerLevel;

    private float timerForShooting;
    private float shootCooldown = 1f;
    private float timeToBeInvincible = 3.0f;
    private bool cooldownHasPassed = true;
    //private PlayerSpawner playerSpawner;
    private GameObject invincibilityAnim;
    private Renderer invincibilityAnimationRenderer;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {

        health = 1;
        speed = 5f;
        projectileSpeed = 0.1f;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //playerSpawner = GetComponent<PlayerSpawner>();
        
        playerIsAlive = true;
        playerIsInvincible = true;
        Debug.Log("playerIsInvincible = true;");

        spawnFreezeIsOver = true;

        // Find the child by name or reference it directly
        invincibilityAnim = transform.Find("InvincibilityAnim").gameObject;

        // Get the Renderer (could be SpriteRenderer, MeshRenderer, etc.)
        invincibilityAnimationRenderer = invincibilityAnim.GetComponent<Renderer>();
        invincibilityAnimationRenderer.enabled = true;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && spawnFreezeIsOver && cooldownHasPassed && !GameLogic.GameOver)
        {
            ShootTheGun();
            cooldownHasPassed = false;
        }

        timerForShooting += Time.deltaTime;
        if (timerForShooting >= shootCooldown)
        {
            timerForShooting = 0;
            cooldownHasPassed = true;
        }

        timeToBeInvincible -= Time.deltaTime;
        if (timeToBeInvincible <= 0)
        {
            Debug.Log("playerIsInvincible = false;");
            playerIsInvincible = false;
            invincibilityAnimationRenderer.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerLevelUp(); 
        }
    }

    void FixedUpdate()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % GetSpriteBasedOnPlayerLevel(playerLevel).Length; //0 
                spriteRenderer.sprite = GetSpriteBasedOnPlayerLevel(playerLevel)[currentFrame];
            }
        }

        RestrictDiagonalMovements();

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (!GameLogic.GameOver && spawnFreezeIsOver)
        {
            PlayerMove(moveDirection);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            playerIsAlive = false;
            PlayerSpawner.playerLives--;
            Debug.Log("playerLives = " + PlayerSpawner.playerLives);
        }
    }

    private void PlayerMove(Vector2 moveDir)
    {

        Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;

        if (!IsBlocked(targetPosition, moveDir))
        {
            rb.MovePosition(targetPosition);
        }

        if (horizontalInput == 1)
        {
            RotatePlayer(horizontalInput, -90);
        }
        else if (horizontalInput == -1)
        {
            RotatePlayer(horizontalInput, 90);
        }

        if (verticalInput == 1)
        {
            RotatePlayer(90, verticalInput);
        }
        else if (verticalInput == -1)
        {
            RotatePlayer(-90, verticalInput);
        }
    }

    private bool IsBlocked(Vector2 targetPos, Vector2 moveDir)
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

        if (hit.collider != null)
        {
            //Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

    private void ShootTheGun()
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }

    private void PlayerLevelUp() 
    {
        if(playerLevel < 4)
        playerLevel++;
        Debug.Log("playerLevel = " + playerLevel);
    }

    private Sprite[] GetSpriteBasedOnPlayerLevel(int playerLevel)
    {
        switch (playerLevel)
        {
            case 0:
                return tankLevelOneSprites;
            case 1:
                return tankLevelTwoSprites;
            case 2:
                return tankLevelThreeSprites;
            case 3:
                return tankLevelFourSprites;
            default:
                return tankLevelOneSprites;
        }
    }
}
