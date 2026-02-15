using UnityEngine;

public class Powerup_Levelup : Powerup_Superclass
{
    private PlayerController2D playerController2D;

    void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            base.OnTriggerEnter2D(other);
            playerController2D.PlayerLevelUp();
            Destroy(gameObject);
        }
    }
}
