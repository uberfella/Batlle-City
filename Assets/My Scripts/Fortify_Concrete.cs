using UnityEngine;

public class Fortify_Concrete : MonoBehaviour, IDamageable, IExplodableTarget
{
    public void TakeDamage(int damage, IDamageSource source)
    {
        if (source.Team == Team.Player)
        {
            if (PlayerProperties.playerLevel < 3)
            {
                return;
            }
            AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
            Destroy(gameObject);
        }
    }
}
