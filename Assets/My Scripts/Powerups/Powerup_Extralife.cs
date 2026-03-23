using UnityEngine;

public class Powerup_Extralife : Powerup_Superclass
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            PlayerProperties.playerLives++;
            Destroy(gameObject);
        }
    }
}
