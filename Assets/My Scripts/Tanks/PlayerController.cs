using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController2D : Tank, IDamageable
{
    public static event Action<PlayerController2D> OnDestroyed;

    public LayerMask obstacleLayer;
    public GameObject shellPrefab;
    public bool playerIsInvincible;
    public bool godmode;
    public Sprite[] tankLevelOneSprites;
    public Sprite[] tankLevelTwoSprites;
    public Sprite[] tankLevelThreeSprites;
    public Sprite[] tankLevelFourSprites;
    public float animationSpeed = 0.2f;
    public SpriteRenderer spriteRenderer;
    public GameObject tankExplosionEffectPrefab;
    public float drawGizmoDistance;
    public float slideWallsDistance;

    private float shootCooldown = 1f;
    private float betweenTwoShotsCooldown = 0.25f;
    private bool cooldownHasPassed = true;
    private bool secondCooldownHasPassed = true;
    private bool betweenTwoShotsCooldownHasPassed = true;
    private GameObject invincibilityAnim;
    private Renderer invincibilityAnimationRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    private Shell shell;
    private GameOverSequence gameOverSequence;
    private Vector2 lastMoveDir;
    private RaycastHit2D lastHit;

    void Start()
    {
        health = 1;
        speed = 2.5f;
        //speed = 5f;

        projectileSpeed = 10f;
        slideWallsDistance = 0.2f;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        shell = GetComponent<Shell>();

        spriteRenderer.sprite = GetSpriteBasedOnPlayerLevel(PlayerProperties.playerLevel)[currentFrame];

        invincibilityAnim = transform.Find("InvincibilityAnim").gameObject;
        invincibilityAnimationRenderer = invincibilityAnim.GetComponent<Renderer>();
        gameOverSequence = FindFirstObjectByType<GameOverSequence>();
        TriggerInvincibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameLogic.GameOver)
            {
                return;
            }
            if (cooldownHasPassed)
            {
                ShootTheGun();
                cooldownHasPassed = false;
                betweenTwoShotsCooldownHasPassed = false;
                StartCoroutine(SetShootingCooldownCoroutine());
                StartCoroutine(SetbetweenTwoShotsCooldownCoroutine());
            }
            else
            {
                if (PlayerProperties.playerLevel >= 2 && betweenTwoShotsCooldownHasPassed && secondCooldownHasPassed)
                {
                    ShootTheGun();
                    secondCooldownHasPassed = false;
                    StartCoroutine(SetSecondShootingCooldownCoroutine());
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    godmode = false;
        //    TriggerInvincibility();
        //}
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
                var sprites = GetSpriteBasedOnPlayerLevel(PlayerProperties.playerLevel);
                if (sprites.Length > 0)
                {
                    currentFrame = (currentFrame + 1) % GetSpriteBasedOnPlayerLevel(PlayerProperties.playerLevel).Length; //0 
                    spriteRenderer.sprite = GetSpriteBasedOnPlayerLevel(PlayerProperties.playerLevel)[currentFrame];   
                }
            }
        }

        RestrictDiagonalMovements();

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (!GameLogic.GameOver /*&& spawnFreezeIsOver*/)
        {
            PlayerMove(moveDirection);
        }
    }

    public void TakeDamage(int damage, IDamageSource source)
    {
        if (source.Team == Team.Player)
        {
            return;
        }
        if (playerIsInvincible)
        {
            return;
        }

        health -= damage;
        if (health <= 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.playerExplodeSound);
            Instantiate(tankExplosionEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            PlayerProperties.playerLives--;
            PlayerProperties.playerLevel = 0;
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

        Vector2 targetPosition = rb.position + speed * Time.fixedDeltaTime * moveDir;
        Vector2 targetPositionForSliding = targetPosition;

        if (IsBlocked(targetPosition, moveDir))
        {
            //sliding walls
            if (moveDir.x == 0)
            {
                //up or down
                targetPositionForSliding.x = targetPosition.x + slideWallsDistance;
                if (!IsBlocked(targetPositionForSliding, moveDir))
                {
                    rb.MovePosition(targetPositionForSliding);
                }
                else
                {
                    targetPositionForSliding.x = targetPosition.x - slideWallsDistance;
                    if (!IsBlocked(targetPositionForSliding, moveDir))
                    {
                        rb.MovePosition(targetPositionForSliding);
                    }
                }
            }
            else
            {
                //left or right
                targetPositionForSliding.y = targetPosition.y + slideWallsDistance;
                if (!IsBlocked(targetPositionForSliding, moveDir))
                {
                    rb.MovePosition(targetPositionForSliding);
                }
                else
                {
                    targetPositionForSliding.y = targetPosition.y - slideWallsDistance;
                    if (!IsBlocked(targetPositionForSliding, moveDir))
                    {
                        rb.MovePosition(targetPositionForSliding);
                    }
                }
            }
        }
        else
        {
            //regular movement
            rb.MovePosition(targetPosition);
        }

        //player gameobject rotation, sprites always facing up
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
        lastMoveDir = moveDir;
        // Cast a box to detect collisions ahead
        RaycastHit2D hit = Physics2D.BoxCast(
            targetPos,  // Cast from collider center
            boxCollider.bounds.size,    // Use actual collider size
            0f,                         // No rotation
            moveDir,                    // Move direction
            0.05f,                        // Distance to check
            obstacleLayer                // Check against obstacles
        );
        lastHit = hit;

        if (hit.collider != null)
        {
            //Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        //Vector2 direction = Application.isPlaying ? lastMoveDir : Vector2.up;
        Vector2 direction = lastMoveDir;

        if (direction == Vector2.zero)
        {
            direction = transform.up; // force visible direction
        }

        //float distance = 0.1f;

        Vector2 startCenter = boxCollider.bounds.center;
        Vector2 size = boxCollider.bounds.size;
        Vector2 endCenter = startCenter + direction * drawGizmoDistance;

        // Start box (green)
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(startCenter, size);

        // End box (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endCenter, size);

        // Line showing direction
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startCenter, endCenter);

        if (lastHit.collider != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(lastHit.centroid, size);
        }
    }

    private void ShootTheGun()
    {

        GameObject shellObject = Instantiate(shellPrefab, transform.position, transform.rotation);
        Shell shell = shellObject.GetComponent<Shell>();
        shell.Init(Team.Player);
        shellObject.GetComponent<Shell>().SetSpeed(projectileSpeed);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shotFired);

    }

    public void PlayerLevelUp()
    {
        if (PlayerProperties.playerLevel <= 3)
        {
            PlayerProperties.playerLevel++;
            Debug.Log("playerLevel = " + PlayerProperties.playerLevel);
        }
        if (PlayerProperties.playerLevel == 1)
        {
            projectileSpeed += 2.0f;
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
    //public void TriggerShootCooldown()
    //{
    //    StartCoroutine(SetShootingCooldownCoroutine());
    //}
    private IEnumerator SetShootingCooldownCoroutine()
    {
        yield return new WaitForSeconds(shootCooldown);

        cooldownHasPassed = true;
    }

    private IEnumerator SetbetweenTwoShotsCooldownCoroutine()
    {
        yield return new WaitForSeconds(betweenTwoShotsCooldown);

        betweenTwoShotsCooldownHasPassed = true;
    }

    private IEnumerator SetSecondShootingCooldownCoroutine()
    {
        yield return new WaitForSeconds(shootCooldown);

        secondCooldownHasPassed = true;
    }
}
