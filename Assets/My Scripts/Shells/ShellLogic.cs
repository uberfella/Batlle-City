using System;
using UnityEngine;

public class Shell : MonoBehaviour
{

    public float speed = 10f;
    public PlayerController2D playerController2D;
    public GameObject explosionEffectPrefab;
    public Rigidbody2D rb;
    //private ExplosionEffectScript explosionEffect;
    private EnemyLvl1 enemyLvl1;
    private EnemyLvl2 enemyLvl2;
    private EnemyLvl3 enemyLvl3;
    private EnemyLvl4 enemyLvl4;

    protected Collider2D[] objectsHit;

    void Start()
    {
        enemyLvl1 = GetComponent<EnemyLvl1>();
        enemyLvl2 = GetComponent<EnemyLvl2>();
        enemyLvl3 = GetComponent<EnemyLvl3>();
        enemyLvl4 = GetComponent<EnemyLvl4>();
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        rb = GetComponent<Rigidbody2D>();
        //explosionEffect = GetComponent<ExplosionEffectScript>();
    }
    void FixedUpdate()
    {
        FlyForward();
    }

    private void FlyForward()
    {
        rb.linearVelocity = transform.up * speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            target.TakeDamage(1);
        }

        if (other.GetComponent<IExplodableTarget>() != null) 
        {
            Explode();
        }

        Destroy(gameObject);



        //if (gameObject.CompareTag("ShellPlayer"))
        //{
        //    if (IsEnemyTag(other.gameObject.tag))
        //    {
        //        GetTagAndTakeDamage(other.gameObject.tag, other.gameObject);
        //    }
        //    else if (ThingsThatShellExplodeOn(other.gameObject.tag))
        //    {
        //        if (!other.gameObject.CompareTag("Brick") && !other.gameObject.CompareTag("Base"))
        //        {
        //            AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
        //        }
        //        Explode();
        //    }
    }

    protected virtual void Explode(){}

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
}