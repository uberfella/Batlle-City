using UnityEngine;


/*
 TODO
13x13

enemy spawning process and spawn points ✓
enemies and the player get destroyed on hit, destroyed enemies give score ✓
how many spawns are there?
player spawns and lives count
tanks are invincible when spawning
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
