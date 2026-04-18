using UnityEngine;

public class Base : MonoBehaviour, IDamageable, IExplodableTarget
{
    public GameObject surrenderFlagSprite;
    private PlayerController2D playerController2D;
    private GameOverSequence gameOverSequence;

    void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        gameOverSequence = FindFirstObjectByType<GameOverSequence>();
    }

    public void TakeDamage(int damage, IDamageSource source)
    {

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
