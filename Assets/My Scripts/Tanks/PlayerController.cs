using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController2D : Tank
{
    public static event Action<PlayerController2D> OnDestroyed;

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
    public GameObject tankExplosionEffectPrefab;

    private float shootCooldown = 1f;
    private bool cooldownHasPassed = true;
    private GameObject invincibilityAnim;
    private Renderer invincibilityAnimationRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    private Shell shell;
    private GameOverSequence gameOverSequence;

    void Start()
    {

        health = 1;
        //speed = 2.5f;
        speed = 5f;
        projectileSpeed = 10f;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        shell = GetComponent<Shell>();

        playerIsAlive = true;
        spawnFreezeIsOver = true;

        invincibilityAnim = transform.Find("InvincibilityAnim").gameObject;
        invincibilityAnimationRenderer = invincibilityAnim.GetComponent<Renderer>();
        gameOverSequence = FindFirstObjectByType<GameOverSequence>();
        TriggerInvincibility();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && spawnFreezeIsOver && cooldownHasPassed && !GameLogic.GameOver)
        {
            ShootTheGun();
            cooldownHasPassed = false;
            TriggerShootCooldown();
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) 
        {
            godmode = false;
            TriggerInvincibility();
        }
        //when the scene gets switched = all sounds stop
        bool isMoving = horizontalInput != 0 || verticalInput != 0;
        AudioManager.Instance.SetEngineState(isMoving);
    }

    void FixedUpdate()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && !GameLogic.GameOver)
        {
            timer += Time.fixedDeltaTime;
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
            playerIsAlive = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.playerExplodeSound);
            Instantiate(tankExplosionEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            PlayerSpawner.playerLives--;
            //if (PlayerSpawner.playerLives <= 0)
            //{
            //    gameOverSequence.TriggerGameOver();
            //}
        }
    }

    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);

        if (AudioManager.Instance != null)
            AudioManager.Instance.StopEngineSound();
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
        //Debug.Log("cooldownHasPassed = " + cooldownHasPassed);

        GameObject shell = Instantiate(shellPrefab, transform.position, transform.rotation);
        shell.GetComponent<Shell>().SetSpeed(projectileSpeed);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shotFired);

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
    public void TriggerShootCooldown()
    {
        StartCoroutine(SetShootingCooldownCoroutine());
    }
    private IEnumerator SetShootingCooldownCoroutine()
    {
        yield return new WaitForSeconds(shootCooldown);

        cooldownHasPassed = true;
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

public class RaycastVisualizer : MonoBehaviour
{
    public float rayDistance = 10f;
    private RaycastHit2D hit;

    void Update()
    {
        // Perform the raycast (e.g., shooting right from the object)
        Vector2 direction = transform.right;
        hit = Physics2D.Raycast(transform.position, direction, rayDistance);
    }

    // This method draws in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 origin = transform.position;
        Vector2 direction = transform.right;

        // If something was hit, draw up to the hit point
        if (hit.collider != null)
        {
            Gizmos.color = Color.green; // Change color to green on hit
            Gizmos.DrawLine(origin, hit.point);
            // Optionally draw a small sphere at the hit point
            Gizmos.DrawWireSphere(hit.point, 0.2f);
        }
        else
        {
            // Otherwise draw the full length
            Gizmos.DrawRay(origin, direction * rayDistance);
        }
    }
}