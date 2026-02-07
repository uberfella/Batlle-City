using UnityEngine;

public class Powerup_Levelup : MonoBehaviour
{
    private PlayerController2D playerController2D;

    void Awake()
    {

        playerController2D = FindFirstObjectByType<PlayerController2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.levelUpJingle);
            playerController2D.PlayerLevelUp();
            Destroy(gameObject);
        }
    }
}
