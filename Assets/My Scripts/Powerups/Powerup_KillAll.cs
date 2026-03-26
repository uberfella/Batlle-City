using UnityEngine;

public class Powerup_KillAll : Powerup_Superclass
{
    private Enemy enemy;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
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
            Enemy.DestroyAllInLayer();
            Destroy(gameObject);
        }
    }
}
