using UnityEngine;

public class BrickDestroyOnHit : MonoBehaviour, IDamageable, IExplodableTarget
{
    [SerializeField]private bool brickIsBase;
    public void TakeDamage(int damage, IDamageSource source)
    {

        if (source.Team == Team.Player)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.brickDestroyedSound);
        }
        Destroy(gameObject);
    }

}
