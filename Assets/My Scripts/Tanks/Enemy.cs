using System;
using UnityEngine;

public class Enemy : Tank
{
    public GameObject shellPrefab;
    public int scoreOnDestroy;
    public Vector2 movement;
    public AiController aiController;
    public LayerMask obstacleLayer;
    public bool hasPowerup;
    public EnemyType enemyType;
    protected bool objectIsCurrentlyBeingBlocked;
    private Spawner spawner;
    private PowerupLogic powerupLogic;
    protected bool isFrozen = false;

    void Awake()
    {
        spawner = FindFirstObjectByType<Spawner>();
        powerupLogic = FindFirstObjectByType<PowerupLogic>();
    }

    void OnEnable()
    {
        Spawner.AliveEnemies.Add(this);
    }

    void OnDisable() // called on death or destroy
    {
        Spawner.AliveEnemies.Remove(this);
    }


    public enum EnemyType
    {
        EnemyLvl1,
        EnemyLvl2,
        EnemyLvl3,
        EnemyLvl4
    }

    public void SetFrozen(bool frozen)
    {
        isFrozen = frozen;
    }

    void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        if (hasPowerup) 
        {
            powerupLogic.SpawnRandomPowerupOnField();
            hasPowerup = false;
        }
        // 4 3 2 1
        health -= damage;
        if (health <= 0)
        {
            GameLogic.Instance.RegisterEnemyKill(enemyType);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyExplodeSound);
            Destroy(gameObject);
            enemyIsAlive = false;
            ChangeEnemyStatus();
        }
    }
    protected void ShootTheGun()
    {
        Instantiate(shellPrefab, transform.position, transform.rotation);
    }

    protected void EnemyMove(Vector2 moveDir)
    {
        //possible values for both inputs are -1, 0, 1
        Vector2 targetPosition = (Vector2)transform.position + moveDir * speed * Time.deltaTime;

        //Debug.Log("horizontalInput = " + horizontalInput);
        //Debug.Log("verticalInput = " + verticalInput);  

        if (!IsBlocked(targetPosition, moveDir))
        {
            transform.position = targetPosition;
        }

        if (IsBlocked(targetPosition, moveDir))
        {
            objectIsCurrentlyBeingBlocked = true;
        }
        else
        {
            objectIsCurrentlyBeingBlocked = false;
        }

        //make the tank sprite face left or right depending on direction 
        if (horizontalInput == 1)
        {
            RotatePlayer(horizontalInput, -90);
        }
        else if (horizontalInput == -1)
        {
            RotatePlayer(horizontalInput, 90);
        }


        //make the tank sprite face up or down depending on direction 
        if (verticalInput == 1)
        {
            RotatePlayer(90, verticalInput);
        }
        else if (verticalInput == -1)
        {
            RotatePlayer(-90, verticalInput);
        }

    }

    protected bool IsBlocked(Vector2 targetPos, Vector2 moveDir)
    {
        float checkDistance = Mathf.Max(speed * Time.deltaTime, 0.2f);

        // Cast a box to detect collisions ahead
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,  // Cast from collider center
            boxCollider.bounds.size,    // Use actual collider size
            0f,                         // No rotation
            moveDir,                    // Move direction
            checkDistance,
            //0.1f,                        // Distance to check
            obstacleLayer                // Check against obstacles
        );

        if (hit.collider != null)
        {
            //Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

    public void ChangeEnemyStatus()
    {
        switch (gameObject.layer)
        {
            case 7:
                //Debug.Log("7 is false");
                Spawner.enemyAlive[0] = false;
                break;
            case 10:
                //Debug.Log("10 is false");
                Spawner.enemyAlive[1] = false;
                break;
            case 11:
                //Debug.Log("11 is false");
                Spawner.enemyAlive[2] = false;
                break;
            case 12:
                //Debug.Log("12 is false");
                Spawner.enemyAlive[3] = false;
                break;
        }
    }

    public static void DestroyAllInLayer()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == 7 || 
                obj.layer == 10 ||
                obj.layer == 11 ||
                obj.layer == 12)
            {
                Debug.Log("Destroying: " + obj.name);
                Enemy script = obj.GetComponent<Enemy>();
                if (script != null)
                {
                    script.ChangeEnemyStatus();
                    script.TakeDamage(5);
                }
            }
        }
    }
}
