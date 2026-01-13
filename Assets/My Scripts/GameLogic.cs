using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
TODO
enemy spawning process and spawn points ✓
enemies and the player get destroyed on hit, destroyed enemies give score ✓
how many spawns are there? 3 spawns but 4 concurrent enemies. the fourth enemy spawns on random spawn 1-3 ✓
are the enemies layers consistent? ✓
player spawns and lives count ✓
ui for lives and enemy count ✓
base sprite turns into flag of surrender when destroyed ✓
player is invincible for 3 seconds when spawning.✓
enemy and player animations ✓
different types of enemies and enemy type presets for each level ✓
spawning animations before enemy spawn ✓
score counting ✓
powerups ✓
powerups disappear after 10 seconds ✓
godmode ✓
main menu and levels system
    nullpointreference for levelnum text when starting the game over
    no gameover in level2 and level3
    no destroyed base sprite in level2 and level3
does the game gets saved when finishing the game?
sounds 
friendly and enemy projectiles will destroy each other when they collide in midair ✓
player levelup, changing sprite and damage ✓
AI - enemy changes direction if it is facing the obstacle for more than 0.5 sec ✓
AI - enemy changes direction if a random time value between 1 and 9 seconds has passed
sprites - use sprite atlas to avoid visible breaks between individual sprites 
remove physics from tanks so you can't bump into them and move their bodies ✓
get rid of objectIsCurrentlyBeingBlocked bool ✓ (cant do that)
main menu - main menu appears from the bottom of the screen
clean code
rename sprites, variables, scripts like enemyLvl0-3 to one standard so it would make more sense
freeze powerup affects only tanks that are alive not the ones that would spawn after picking up the powerup
get rid of constant checking variables' values in Update() method, replace em with Coroutines
refactor enemy spawn anim
power-ups spawn on a grid; for example, the spawn position should be divisible by 0.5, 1.0, 1.5 without any remainder
leveledup player can destroy concrete?

QUESTIONS
do enemies have different projectile speeds ? 
does player collider changes when leveling up?
 * 
 * 
 * 
 * 
GAME SPEC
Game Plane is 13x13 in size
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

    public static GameLogic Instance { get; private set; }
    public bool isEnemiesFrozen;
    public Text levelNumText;
    public RectTransform gameOverText;
    public float moveDuration = 5.5f;
    public Vector2 targetPosition;

    private Vector2 startPosition;
    private Spawner spawner;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    Debug.Log("no instance found, assigning a new one");
        //}
        //else 
        //{
        //    Destroy(gameObject);
        //    Debug.Log("old instance found, destroying it...");
        //}
        //DontDestroyOnLoad(gameObject);
        spawner = FindFirstObjectByType<Spawner>();
    }
    void Start()
    {
        startPosition = gameOverText.anchoredPosition;
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (levelNumText.text != null)
        if (BootstrappedData.levelNum < BootstrappedData.finalLevelNum)
        {
            levelNumText.text = (BootstrappedData.levelNum + 1).ToString();
        }
        



    }

    public void TriggerGameOver()
    {
        //Debug.Log("trying to game over");
        if (!GameOver && !spawner.levelFinished)
        {
            GameOver = true;
            SaveManager.EraseSave();
            StartCoroutine(ShowGameOver());
            
        }
    }

    IEnumerator ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        yield return StartCoroutine(MoveText(startPosition, targetPosition, moveDuration));
        SceneManager.LoadScene("Main Menu");
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
