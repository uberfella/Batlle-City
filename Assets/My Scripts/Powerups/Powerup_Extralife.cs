using UnityEngine;

public class Powerup_Extralife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerSpawner.playerLives++;
        Destroy(gameObject);
    }
}
