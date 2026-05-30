using System.Collections;
using UnityEngine;

public class Powerup_Superclass : MonoBehaviour
{

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.powerUpPickup);

    }

    public virtual void DestroyPowerup(int damage)
    {
        Destroy(gameObject);
    }

    protected IEnumerator SelfDestroyInXSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyPowerup(1);
    }
}