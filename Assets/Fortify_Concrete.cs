using UnityEngine;

public class Fortify_Concrete : MonoBehaviour, IDamageable, IExplodableTarget
{
    public void TakeDamage(int damage, IDamageSource source)
    {
        if (source.Team == Team.Player)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
            if (PlayerProperties.playerLevel >= 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
