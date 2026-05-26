using UnityEngine;

public class Powerup_Levelup : Powerup_Superclass, IDamageable
{
    private PlayerController2D playerController2D;
    public SpriteRenderer spriteRenderer;


    void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
    }
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            playerController2D.PlayerLevelUp();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage, IDamageSource source)
    {

        Destroy(gameObject);

    }
}
