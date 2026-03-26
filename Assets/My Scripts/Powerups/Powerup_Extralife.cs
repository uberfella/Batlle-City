using System.Collections;
using UnityEngine;

public class Powerup_Extralife : Powerup_Superclass
{
    public SpriteRenderer spriteRenderer;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            PlayerProperties.playerLives++;
            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }

}
