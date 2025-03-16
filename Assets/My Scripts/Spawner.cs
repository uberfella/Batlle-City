using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabLvl1;
    public Transform[] spawnPoints;
    public int enemiesToSpawn = 10;
    public int enemiesOnTheField = 4;
    public bool[] enemyAlive = new bool[4] { false, false, false, false }; //7, 10, 11, 12
    public LayerMask obstructionMask;

    private float timer = 0f;
    private float cooldownToSpawn = 5f;

    void Start()
    {
        for (int i = 0; i < enemiesOnTheField; i++)
        {
            SpawnEnemy(i);
        }

    }

    void Update()
    {
        for (int i = 0; i < enemyAlive.Length; i++)
        {
            if (!enemyAlive[i] && enemiesToSpawn > 0)
            {
                timer += Time.deltaTime;
                if (timer >= cooldownToSpawn)
                {
                    SpawnEnemy(i);
                    enemiesToSpawn--;
                    Debug.Log("enemies to spawn: " + enemiesToSpawn);
                    timer = 0f;
                }

            }
        }

    }

    public void SpawnEnemy(int index)
    {

        float checkRadius = 1.0f;

        Transform spawnPoint = spawnPoints[index];
        //GameObject newEnemy = Instantiate(enemyPrefabLvl1[index], spawnPoint.position, Quaternion.identity);
        //enemyAlive[index] = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, checkRadius, obstructionMask);

        if (colliders.Length == 0)  // No obstructions
        {
            GameObject newEnemy = Instantiate(enemyPrefabLvl1[index], spawnPoint.position, Quaternion.identity);
            enemyAlive[index] = true;
        }
        else
        {
            Debug.Log("Spawn point is obstructed. Try again later.");
        }
    }

}

/*
4 spawn points
each point belongs to a separate enemy, enemy index
if the enemy with corresponding index dies the new one gets to spawn

there's an x second cooldown before the enemy spawns
this cooldown is shared between all the to-be-spawned enemies
 */