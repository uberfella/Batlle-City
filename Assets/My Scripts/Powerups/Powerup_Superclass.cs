using UnityEngine;

public class Powerup_Superclass : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.powerUpPickup);
        }
    }
}
