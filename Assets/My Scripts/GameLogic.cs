using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enemy;

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
main menu and levels system ✓
    nullpointreference for levelnum text when starting the game over ✓
    no gameover in level2 and level3 ✓
    no destroyed base sprite in level2 and level3 ✓
scoreboard on finishing the level with breakdown of each enemy type kill ✓
freeze powerup affects only tanks that are alive not the ones that would spawn after picking up the powerup ✓
power-ups spawn on a grid; for example, the spawn position should be divisible by 0.5, 1.0, 1.5 without any remainder✓
sounds 
what if the player reaches final level, gets his game saved and then returns to main menu and then continues the game
when the new powerup spawns the previous powerup gets destroyed
when the player stops moving or changes directions the shooting cooldown gets reset so it's possible to shoot the second time before the cooldown ends
finish game screen
friendly and enemy projectiles will destroy each other when they collide in midair ✓
player levelup, changing sprite and damage ✓
AI - enemy changes direction if it is facing the obstacle for more than 0.5 sec ✓
AI - enemy changes direction if a random time value between 1 and 9 seconds has passed
sprites - use sprite atlas to avoid visible breaks between individual sprites 
remove physics from tanks so you can't bump into them and move their bodies ✓
get rid of objectIsCurrentlyBeingBlocked bool ✓ (cant do that)
main menu - main menu appears from the bottom of the screen
clean code
    Enemy update() unification?
    rename sprites, variables, scripts like enemyLvl0-3 to one standard so it would make more sense
    get rid of constant checking variables' values in Update() method, replace em with Coroutines
    refactor enemy spawn anim



QUESTIONS
do enemies have different projectile speeds ? yes
does player collider changes when leveling up? ✓ no
does high score gets overriden in real time or it does only when player finishes the level ✓ only when finishing the level either by killing everyone or losing it
does the game gets saved when finishing the game? ✓ why would it, erase the game when finishing
leveledup player can destroy steel wall? yes, on tier four levelup
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

public static class PerformBootstrap
{
    const string SceneName = "Bootstrapped Scene";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        // traverse the currently loaded scenes
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
        {
            var candidate = SceneManager.GetSceneAt(sceneIndex);

            if (candidate.name == SceneName)
            {
                return;
            }
        }

        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
    }
}

public class GameLogic : MonoBehaviour
{
    public static bool GameOver;
    public static GameLogic Instance { get; private set; } = null;
    public static int levelNum = 0;
    public static int finalLevelNum = 1; //finalLevel 3 == End scene
    public bool isEnemiesFrozen;
    public Dictionary<EnemyType, int> destroyedByType = new();
    private PlayerController2D playerController2D;

    private void Awake()
    {
        // check if an instance already exists
        if (Instance != null)
        {
            Debug.LogError("Found another BootstrappedData on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // prevent the data from being unloaded
        DontDestroyOnLoad(gameObject);

        

    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {

            Debug.Log("highScoreHasBeenBeaten = " + ScoreCount.highScoreHasBeenBeaten);
        }

    }
    public void RegisterEnemyKill(EnemyType type)
    {
        if (!destroyedByType.ContainsKey(type))
            destroyedByType[type] = 0;

        destroyedByType[type]++;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsScoreboardScene(scene) && !GameOver)
        {
            SaveManager.SaveGame();
        }

    }

    private bool IsScoreboardScene(Scene scene)
    {
        return scene.name.StartsWith("Score");
        // or use build index, or a list, your choice
    }
}
