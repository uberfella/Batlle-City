using UnityEngine;

public class ShellEnemy : Shell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLvl1") ||
            other.CompareTag("EnemyLvl2") ||
            other.CompareTag("EnemyLvl3") ||
            other.CompareTag("EnemyLvl4"))
            return;

        IDamageablePlayer targetPlayer = other.GetComponent<IDamageablePlayer>();

        if (targetPlayer != null)
        {
            targetPlayer.TakeDamage(1);
        }

        base.OnTriggerEnter2D(other);
    }

    protected override void Explode()
    {
        Vector2 explosionCenter = transform.position;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f); // 2.0f left, 2.0f right, 0.5f forward
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(explosionCenter, explosionSize, transform.eulerAngles.z);

        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

        foreach (Collider2D obj in objectsHit)
        {
            if (obj.name.Contains("Concr"))
            {
                continue;
            }
            //Debug.Log("obj = " + obj);
            if (obj.GetComponent<IDamageable>() != null)
            {
                IDamageable target = obj.GetComponent<IDamageable>();
                target.TakeDamage(1, this);
            }
            if (obj.GetComponent<IDamageablePlayer>() != null)
            {
                IDamageablePlayer target = obj.GetComponent<IDamageablePlayer>();
                target.TakeDamage(1);
            }
        }
    }
}
