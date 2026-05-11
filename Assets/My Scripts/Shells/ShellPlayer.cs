using UnityEngine;

public class ShellPlayer : Shell, IDamageable
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        base.OnTriggerEnter2D(other);
    }

    public void TakeDamage(int damage, IDamageSource source)
    {
        Destroy(gameObject);
    }
}
