using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject surrenderFlagSprite;
    private PlayerController2D playerController2D;
    private GameLogic gameLogic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        gameLogic = FindFirstObjectByType<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShellPlayer") || other.gameObject.CompareTag("ShellEnemy"))
        {
            if (!playerController2D.godmode) 
            {
                surrenderFlagSprite.GetComponent<SpriteRenderer>().enabled = true;
                //Destroy(gameObject);
                //GameLogic.GameOver = true;
                gameLogic.TriggerGameOver();
            }
        }
    }
}
