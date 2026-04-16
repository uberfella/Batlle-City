using UnityEngine;

public class ConcreteDestroyOnHit : MonoBehaviour, IDamageable, IExplodableTarget
{
    public void TakeDamage(int damage, IDamageSource source)
    {
        if (source.Team == Team.Player)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
            if (PlayerProperties.playerLevel >= 3)
            {
                Debug.Log("PlayerProperties.playerLevel == " + PlayerProperties.playerLevel);
                Destroy(gameObject);
            }
        }
    }
}   
