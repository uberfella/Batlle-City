using UnityEngine;

public class Base : MonoBehaviour, IDamageable, IExplodableTarget
{
    public GameObject surrenderFlagSprite;
    public PlayerController2D playerController2D;
    public GameOverSequence gameOverSequence;
    public void TakeDamage(int damage, IDamageSource source)
    {
        //this reference becomes null when player gets destroyed but it's fine 
        if (playerController2D.godmode)
        {
            return;
        }

        surrenderFlagSprite.GetComponent<SpriteRenderer>().enabled = true;
        gameOverSequence.TriggerGameOver();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerExplodeSound);
        Destroy(gameObject);
    }
}
