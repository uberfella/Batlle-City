using UnityEngine;

public class Powerup_Extralife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSpawner.playerLives++;
            Destroy(gameObject);
        }
    }
}
