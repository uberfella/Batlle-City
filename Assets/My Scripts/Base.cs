using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject surrenderFlagSprite;
    private PlayerController2D playerController2D;
    private GameLogic gameLogic;

    void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        gameLogic = FindFirstObjectByType<GameLogic>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("ShellPlayer") || other.gameObject.CompareTag("ShellEnemy"))
        {
            Debug.Log("trying to destroy base");
            if (playerController2D.godmode)
            {
                Debug.Log("godmode is on, returning");
                return;
            }

            surrenderFlagSprite.GetComponent<SpriteRenderer>().enabled = true;
            gameLogic.TriggerGameOver();

        }
    }
}
