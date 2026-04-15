using UnityEngine;

public class Fortify_Concrete : MonoBehaviour, IDamageable, IExplodableTarget
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("ShellPlayer"))
        //{
        //    AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
        //    if (PlayerProperties.playerLevel >= 3)
        //    {
        //        TakeDamage(1);
        //    }
        //}
    }

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
