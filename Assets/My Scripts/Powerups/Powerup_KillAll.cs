using UnityEngine;

public class Powerup_KillAll : Powerup_Superclass, IDestroyablePowerup
{
    private Enemy enemy;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void Start()
    {
        StartCoroutine(SelfDestroyInXSeconds(10f));
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
            DestroyAllInLayer();
            Destroy(gameObject);
        }
    }

    public void DestroyAllInLayer()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == 7 ||
                obj.layer == 10 ||
                obj.layer == 11 ||
                obj.layer == 12)
            {
                Enemy enemy = obj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ChangeEnemyStatus();
                    var source = new SimpleDamageSource(gameObject, Team.Player);
                    enemy.TakeDamage(5, source);
                }
            }
        }
    }
}
