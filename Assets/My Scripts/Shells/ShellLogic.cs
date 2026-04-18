using System;
using UnityEngine;

public class Shell : MonoBehaviour, IDamageSource
{
    private float speed = 10f;
    public GameObject explosionEffectPrefab;
    public Rigidbody2D rb;

    public GameObject Owner { get; private set; }

    public void Init(Team team)
    {
        Team = team;
    }

    public Team Team { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            target.TakeDamage(1, this);
            Destroy(gameObject);

        }

        if (other.GetComponent<IExplodableTarget>() != null) 
        {
            Explode();
            Destroy(gameObject);

        }
    }

    private void Explode()
    {
        Vector2 explosionCenter = transform.position;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f);
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(explosionCenter, explosionSize, transform.eulerAngles.z);

        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

        foreach (Collider2D obj in objectsHit)
        {
            if (obj.GetComponent<IDamageable>() != null)
            {
                IDamageable target = obj.GetComponent<IDamageable>();
                target.TakeDamage(1, this);
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
}