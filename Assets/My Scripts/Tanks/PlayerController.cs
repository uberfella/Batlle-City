using System.Collections;
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
    public bool godmode;
    public Sprite[] tankLevelOneSprites;
    public Sprite[] tankLevelTwoSprites;
    public Sprite[] tankLevelThreeSprites;
    public Sprite[] tankLevelFourSprites;
    public float animationSpeed = 0.2f; // Time between frames
    public SpriteRenderer spriteRenderer;
    public static int playerLevel;

    private float timerForShooting;
    private float shootCooldown = 1f;
    private bool cooldownHasPassed = true;
    //private PlayerSpawner playerSpawner;
    private GameObject invincibilityAnim;
    private Renderer invincibilityAnimationRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    private Shell shell;
    private AudioManager audioManager;

    void Start()
    {

        health = 1;
        speed = 5f;
        projectileSpeed = 10f;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        shell = GetComponent<Shell>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        playerIsAlive = true;
        spawnFreezeIsOver = true;

        invincibilityAnim = transform.Find("InvincibilityAnim").gameObject;
        invincibilityAnimationRenderer = invincibilityAnim.GetComponent<Renderer>();
        TriggerInvincibility();
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

        if (Input.GetKeyDown(KeyCode.Backspace)) 
        {
            godmode = false;
            TriggerInvincibility();
        }
        if ((horizontalInput != 0 || verticalInput != 0))
        {
            
        }
    }

    void FixedUpdate()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && !GameLogic.GameOver)
        {
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0f;
                var sprites = GetSpriteBasedOnPlayerLevel(playerLevel);
                if (sprites.Length > 0)
                {
                    currentFrame = (currentFrame + 1) % GetSpriteBasedOnPlayerLevel(playerLevel).Length; //0 
                    spriteRenderer.sprite = GetSpriteBasedOnPlayerLevel(playerLevel)[currentFrame];
                }
            }
        }

        bool isMoving = horizontalInput != 0 || verticalInput != 0;
        AudioManager.Instance.SetEngineState(isMoving);

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
        GameObject shell = Instantiate(shellPrefab, transform.position, transform.rotation);
        shell.GetComponent<Shell>().SetSpeed(projectileSpeed);
        audioManager.PlaySFX(audioManager.shotFired);
    }

    public void PlayerLevelUp() 
    {
        if (playerLevel <= 4)
        {
            playerLevel++;
            projectileSpeed += 2.5f;
            Debug.Log("playerLevel = " + playerLevel);
        }
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

    public void TriggerInvincibility()
    {
        StartCoroutine(InvincibilityCoroutine());
    }
    private IEnumerator InvincibilityCoroutine()
    {
        playerIsInvincible = true;
        invincibilityAnimationRenderer.enabled = true;

        if (!godmode)
        {
            yield return new WaitForSeconds(5f);

            playerIsInvincible = false;
            invincibilityAnimationRenderer.enabled = false;
        }

    }

    //void SwitchToMove()
    //{
    //    engineIdleSource.Pause();
    //    if (!engineMoveSource.isPlaying)
    //        engineMoveSource.Play();
    //}

    //void SwitchToIdle()
    //{
    //    engineMoveSource.Pause();
    //    if (!engineIdleSource.isPlaying)
    //        engineIdleSource.Play();
    //}

}
