using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage, IDamageSource source);
}
