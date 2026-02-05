using UnityEngine;

public class Powerup_Levelup : MonoBehaviour
{
    private PlayerController2D playerController2D;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        playerController2D = FindFirstObjectByType<PlayerController2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            audioManager.PlaySFX(audioManager.levelUpJingle);
            playerController2D.PlayerLevelUp();
            Destroy(gameObject);
        }
    }
}
