using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/*
 TODO
13x13

enemy spawning process and spawn points ✓
enemies and the player get destroyed on hit, destroyed enemies give score ✓
how many spawns are there? 3 spawns but 4 concurrent enemies. the fourth enemy spawns on random spawn 1-3 ✓
are the enemies layers consistent? ✓
player spawns and lives count ✓
ui for lives and enemy count ✓
base sprite turns into flag of surrender when destroyed
tanks are invincible and frozen when spawning
friendly and enemy projectiles will cancel each other out when they collide in midair ✓
player powerup, changing sprite and damage (?)
Ai - enemy changes direction if facing the obstacle for more than 0.5 sec ✓
sprites - use sprite atlas to avoid visible breaks between individual sprites 
remove physics from tanks so you can't bump into them and move their bodies ✓
get rid of objectIsCurrentlyBeingBlocked bool
 * 
 * 
 * 
 * 
GAME SPEC
You start out at the bottom of the screen next to your base. Enemy tanks will appear from one of three positions at the top of the screen.
In each stage, there are 20 tanks in total you must defeat in order to advance to the next stage.
You can fire in four directions. Most tanks only require one hit to destroy them. One type of tank requires four hits.
If one enemy bullet hits you, you lose one life. If your base is ever hit by a bullet, the game is automatically over.
Bullets can destroy walls, whether they are fired by you or the enemy. It takes four shots to break through a standard width wall.
Enemy tanks that flash red provide power-ups whenever hit. The power up will appear randomly somewhere on the screen.
 */
public class GameLogic : MonoBehaviour
{
    public static bool GameOver;
    public static int levelNum = 1;
    public static int currentScore = 0;
    public static int highScore = 0;
    public Text levelNumText;
    public Text currentScoreText;
    public Text highScoreText;
    public RectTransform gameOverText;
    public float moveDuration = 1.5f;
    public Vector2 targetPosition;
    private Vector2 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = gameOverText.anchoredPosition;
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        levelNumText.text = levelNum.ToString();
        currentScoreText.text = currentScore.ToString("D6");
        highScoreText.text = highScore.ToString("D6");

        if(GameOver) 
        {
            ShowGameOver();
        }
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(MoveText(startPosition, targetPosition, moveDuration));
    }

    IEnumerator MoveText(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            gameOverText.anchoredPosition = Vector2.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameOverText.anchoredPosition = to; // Ensure it reaches the final position
    }
}
