using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabLvl1;
    public GameObject[] enemyPrefabLvl2;
    public GameObject[] enemyPrefabLvl3;
    public GameObject[] enemyPrefabLvl4;
    public Transform[] spawnPoints;
    public GameObject[] spawnPointsObjects;
    public int enemiesToSpawn = 0;
    public Text enemiesToSpawnText;
    //TODO get rid of enemiesOnTheField
    public int enemiesOnTheField = 4;
    public static bool[] enemyAlive = new bool[4] { false, false, false, false }; //7, 10, 11, 12
    public LayerMask obstructionMask;
    public EnemiesList enemiesList;

    private float timer = 0f;
    private float cooldownToSpawn = 5f;
    private int iterateOverSpawnList = 0;
    private int enemyIdToSpawn = 0;
    private GameObject[] spawnAnim;
    private Renderer[] spawnAnimationRenderer;
    private int randomSpawnPoint = 0;

    Animator animator;

    void Start()
    {

        enemiesList = FindFirstObjectByType<EnemiesList>();
        spawnAnim = new GameObject[3];
        spawnAnimationRenderer = new Renderer[3];
        for (int i = 0; i < 3; i++)
        {
            spawnAnim[i] = GameObject.Find($"Spawn{i}");
            spawnAnimationRenderer[i] = spawnAnim[i].GetComponent<Renderer>();
        }

    }

    void Update()
    {

        enemiesToSpawnText.text = enemiesToSpawn.ToString();

        for (int i = 0; i < enemyAlive.Length; i++)
        {
            if (!enemyAlive[i] && enemiesToSpawn > 0)
            {
                timer += Time.deltaTime;

                if (timer > cooldownToSpawn - 2)
                {
                    if (i == 3)
                    {
                        spawnAnimationRenderer[randomSpawnPoint].enabled = true;
                    }
                    else
                    {
                        spawnAnimationRenderer[i].enabled = true;
                    }
                }

                if (timer >= cooldownToSpawn)
                {
                    SpawnEnemy(i);
                    if (i == 3)
                    {
                        spawnAnimationRenderer[randomSpawnPoint].enabled = false;
                    }
                    else
                    {
                        spawnAnimationRenderer[i].enabled = false;
                    }

                    //enemiesToSpawn--;
                    //Debug.Log("enemies to spawn: " + enemiesToSpawn);
                    timer = 0f;
                }

            }
        }

    }

    public void SpawnEnemy(int index)
    {

        float checkRadius = 1.0f;

        Transform spawnPoint = spawnPoints[index];

        //dynamic spawnPoint
        if (index == 3)
        {
            /*int */
            randomSpawnPoint = Random.Range(0, 3);
            spawnPoint = spawnPoints[randomSpawnPoint];
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, checkRadius, obstructionMask);

        if (colliders.Length == 0 && iterateOverSpawnList < enemiesList.GetEnemiesListForLevel(GameLogic.levelNum).Length)  // No obstructions
        {
            enemyIdToSpawn = enemiesList.GetEnemiesListForLevel(GameLogic.levelNum)[iterateOverSpawnList]; //0 1 2 3 4 5 6 7
            iterateOverSpawnList++;
            GameObject newEnemy = Instantiate(GetPrefabTypeById(enemyIdToSpawn)[index], spawnPoint.position, Quaternion.identity);

            EnemyLvl1 scriptEnemyLvl1 = newEnemy.GetComponent<EnemyLvl1>();
            EnemyLvl2 scriptEnemyLvl2 = newEnemy.GetComponent<EnemyLvl2>();
            EnemyLvl3 scriptEnemyLvl3 = newEnemy.GetComponent<EnemyLvl3>();
            EnemyLvl4 scriptEnemyLvl4 = newEnemy.GetComponent<EnemyLvl4>();

            if (enemyIdToSpawn % 2 != 0)
            {
                if (enemyIdToSpawn == 0 || enemyIdToSpawn == 1)
                {
                    scriptEnemyLvl1.hasPowerup = true;
                }
                if (enemyIdToSpawn == 2 || enemyIdToSpawn == 3)
                {
                    scriptEnemyLvl2.hasPowerup = true;
                }
                if (enemyIdToSpawn == 4 || enemyIdToSpawn == 5)
                {
                    scriptEnemyLvl3.hasPowerup = true;
                }
                if (enemyIdToSpawn == 6 || enemyIdToSpawn == 7)
                {
                    scriptEnemyLvl4.hasPowerup = true;
                }
            }
            else if (enemyIdToSpawn % 2 == 0)
            {
                if (enemyIdToSpawn == 0 || enemyIdToSpawn == 1)
                {
                    scriptEnemyLvl1.hasPowerup = false;
                }
                if (enemyIdToSpawn == 2 || enemyIdToSpawn == 3)
                {
                    scriptEnemyLvl2.hasPowerup = false;
                }
                if (enemyIdToSpawn == 4 || enemyIdToSpawn == 5)
                {
                    scriptEnemyLvl3.hasPowerup = false;
                }
                if (enemyIdToSpawn == 6 || enemyIdToSpawn == 7)
                {
                    scriptEnemyLvl4.hasPowerup = false;
                }
            }

            enemiesToSpawn--;
            enemyAlive[index] = true;
        }
        else
        {
            Debug.Log("Spawn point is obstructed. Try again later.");
            //Debug.Log("colliders.Length = " + colliders.Length);
            //Debug.Log("iterateOverSpawnList = "+ iterateOverSpawnList);
            //Debug.Log("enemiesList.GetEnemiesListForLevel(GameLogic.levelNum).Length = "+ enemiesList.GetEnemiesListForLevel(GameLogic.levelNum).Length);
        }
    }

    //int random = Random.Range(minValForInput, maxValForInput);
    //spawnPoint[3]

    //0 enemyLvl1 regular || even
    //1 enemyLvl1 powerup || uneven
    //2 enemyLvl2 regular || even
    //3 enemyLvl2 powerup || uneven
    //4 enemyLvl3 regular || even
    //5 enemyLvl3 powerup || uneven
    //6 enemyLvl4 regular || even
    //7 enemyLvl4 powerup || uneven
    private GameObject[] GetPrefabTypeById(int id)
    {
        switch (id)
        {
            case 0:
            case 1:
                return enemyPrefabLvl1;
            case 2:
            case 3:
                return enemyPrefabLvl2;
            case 4:
            case 5:
                return enemyPrefabLvl3;
            case 6:
            case 7:
                return enemyPrefabLvl4;
            default:
                return null;
        }
    }



}

/*
3 static spawn points, extra dynamic one
each point belongs to a separate enemy, enemy index
if the enemy with corresponding index dies the new one gets to spawn

there's an x second cooldown before the enemy spawns
this cooldown is shared between all the to-be-spawned enemies
 */