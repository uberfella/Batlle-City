using UnityEngine;

public class ShellEnemy : Shell, IDamageable
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLvl1") ||
            other.CompareTag("EnemyLvl2") ||
            other.CompareTag("EnemyLvl3") ||
            other.CompareTag("EnemyLvl4"))
            return;

        base.OnTriggerEnter2D(other);
    }

    public void TakeDamage(int damage, IDamageSource source)
    {
        Destroy(gameObject);
    }
}
