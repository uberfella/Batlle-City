using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject surrenderFlagSprite;
    private PlayerController2D playerController2D;
    private GameOverSequence gameOverSequence;

    void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        gameOverSequence = FindFirstObjectByType<GameOverSequence>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("ShellPlayer") || other.gameObject.CompareTag("ShellEnemy"))
        {
            //Debug.Log("trying to destroy base");
            if (playerController2D.godmode)
            {
                Debug.Log("godmode is on, returning");
                return;
            }

            surrenderFlagSprite.GetComponent<SpriteRenderer>().enabled = true;
            gameOverSequence.TriggerGameOver();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.playerExplodeSound);
            Destroy(gameObject);
        }
    }
}
