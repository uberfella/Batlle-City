using UnityEngine;

public class ConcreteDestroyOnHit : MonoBehaviour, IDamageable, IExplodableTarget
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (PlayerProperties.playerLevel >= 3 && other.gameObject.CompareTag("ShellPlayer"))
        //{
        //    TakeDamage(1);
        //}
    }

    public void TakeDamage(int damage, IDamageSource source)
    {
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.brickDestroyedSound);
        //Destroy(gameObject);
    }
}   
