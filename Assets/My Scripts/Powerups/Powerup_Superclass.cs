using UnityEngine;

public class Powerup_Superclass : MonoBehaviour
{

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.powerUpPickup);

    }
}