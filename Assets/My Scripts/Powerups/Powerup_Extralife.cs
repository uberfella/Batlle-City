using System.Collections;
using UnityEngine;

public class Powerup_Extralife : Powerup_Superclass, IDestroyablePowerup
{
    public SpriteRenderer spriteRenderer;
    private PlayerSpawner playerSpawner;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            PlayerProperties.playerLives++;
            playerSpawner = FindFirstObjectByType<PlayerSpawner>();
            playerSpawner.UpdatePlayerLivesUI();
            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }

    public void DestroyPowerup(int damage)
    {

        Destroy(gameObject);

    }
}
