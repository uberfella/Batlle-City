using System;
using UnityEngine;

public class Shell : MonoBehaviour
{

    public float speed = 10f;
    public PlayerController2D playerController2D;
    public GameObject explosionEffectPrefab;
    //private ExplosionEffectScript explosionEffect;
    private EnemyLvl1 enemyLvl1;
    private EnemyLvl2 enemyLvl2;
    private EnemyLvl3 enemyLvl3;
    private EnemyLvl4 enemyLvl4;

    void Start()
    {
        enemyLvl1 = GetComponent<EnemyLvl1>();
        enemyLvl2 = GetComponent<EnemyLvl2>();
        enemyLvl3 = GetComponent<EnemyLvl3>();
        enemyLvl4 = GetComponent<EnemyLvl4>();
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        //explosionEffect = GetComponent<ExplosionEffectScript>();
    }
    void Update()
    {
        FlyForward();
    }

    private void FlyForward()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        //the shell is player's
        if (gameObject.CompareTag("ShellPlayer"))
        {
            string tag = other.gameObject.tag;
            if (IsEnemyTag(tag))
            {
                GetTagAndTakeDamage(tag, other.gameObject);

                //destroy player's shell
                Destroy(gameObject);
            }
            else if (ThingsThatShellGetsExplodedOn(tag))
            {
                if (!other.gameObject.CompareTag("Brick") && !other.gameObject.CompareTag("Base"))
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
                }
                //shell explodes forward and to the left and right
                Explode();
                //destroy shell
                Destroy(gameObject);
            }
            //destroy both shells if they collide w each other in mid-air
            //no sound 
            else if (other.gameObject.CompareTag("ShellEnemy"))
            {
                Destroy(other.gameObject);
            }
        }
        //the shell is enemy's
        else if (gameObject.CompareTag("ShellEnemy"))
        {
            //the enemy shell hits a player
            if (other.gameObject.CompareTag("Player"))
            {
                //if(other.gameObject.)
                if (!playerController2D.playerIsInvincible)
                {
                    PlayerController2D playerController2D = other.gameObject.GetComponent<PlayerController2D>();
                    playerController2D.TakeDamage(1);

                    //destroy enemy's shell
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.CompareTag("Brick") ||
            other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Base") ||
            other.gameObject.CompareTag("Concrete") ||
            other.gameObject.CompareTag("Fortify_Concrete"))
            {
                //shell explodes forward and to the left and right
                Explode();
                //destroy shell
                Destroy(gameObject);
            }
        }

        //any shell hits an obstacle
        //if (other.gameObject.CompareTag("Brick") ||
        //    other.gameObject.CompareTag("Wall") ||
        //    other.gameObject.CompareTag("Base") ||
        //    other.gameObject.CompareTag("Concrete") ||
        //    other.gameObject.CompareTag("Fortify_Concrete"))
        //{
        //    //shell explodes forward and to the left and right
        //    Explode();
        //    //destroy shell
        //    Destroy(gameObject);
        //}
    }

    private bool ThingsThatShellGetsExplodedOn(string tag)
    {
        Debug.Log("checking tag "+tag);
        if ((tag == "Wall") || (tag == "Brick") || (tag == "Concrete") || (tag == "Base") || (tag == "Fortify_Concrete"))
        {
            Debug.Log("destroy shell");
            return true;
        }
        Debug.Log("dont destroy shell");
        return false;
    }

    private void Explode()
    {

        Vector2 explosionCenter = transform.position;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f); // 2.0f left, 2.0f right, 0.5f forward
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(explosionCenter, explosionSize, transform.eulerAngles.z);

        //TODO optimize
        foreach (Collider2D obj in objectsHit)
        {
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

            //the shell is player's
            if (gameObject.CompareTag("ShellPlayer"))
            {
                if (obj.CompareTag("Brick"))
                {
                    Destroy(obj.gameObject);
                }
                else if (IsEnemyTag(tag))
                {
                    GetTagAndTakeDamage(tag, obj.gameObject);
                }
            }
            else
            //the shell is enemy's
            if (gameObject.CompareTag("ShellEnemy"))
            {
                if (obj.CompareTag("Brick"))
                {
                    Destroy(obj.gameObject);
                }
                if (obj.CompareTag("Player") && !playerController2D.playerIsInvincible)
                {
                    PlayerController2D playerController2D = obj.GetComponent<PlayerController2D>();
                    playerController2D.TakeDamage(1);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, explosionSize);
    }

    public void SetSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    private void GetTagAndTakeDamage(string tag, GameObject other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        switch (tag)
        {
            case "EnemyLvl1":
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
                break;
            case "EnemyLvl2":
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
                break;
            case "EnemyLvl3":
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
                break;
            case "EnemyLvl4":
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
                break;
        }
    }

    bool IsEnemyTag(string tag)
    {
        return tag == "EnemyLvl1" || tag == "EnemyLvl2" || tag == "EnemyLvl3" || tag == "EnemyLvl4";
    }
}
