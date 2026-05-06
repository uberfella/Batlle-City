using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public static bool[] enemyAlive = new bool[4] { false, false, false, false }; //7, 10, 11, 12
    public static List<Enemy> AliveEnemies = new List<Enemy>();
    public LayerMask obstructionMask;
    public EnemiesList enemiesList;
    public bool levelFinished = false;

    private float cooldownToSpawn = 0.5f;
    private int iterateOverSpawnList = 0;
    private int enemyIdToSpawn = 0;
    private int enemyIndexToSpawn = 0;
    private int currentSpawnPoint = 0;
    private GameObject[] spawnAnim;
    private Renderer[] spawnAnimationRenderer;
    private bool spawnPointHasEnoughRoom = false;
    private bool spawnEnemyProcessIsInProgress = false;
    private Transform spawnPoint;
    private bool loadingNextLevelIsInProgress = false;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            enemyAlive[i] = false;
        }
        enemiesList = FindFirstObjectByType<EnemiesList>();
        spawnAnim = new GameObject[3];
        spawnAnimationRenderer = new Renderer[3];
        for (int i = 0; i < 3; i++)
        {
            spawnAnim[i] = GameObject.Find($"Spawn{i}");
            spawnAnimationRenderer[i] = spawnAnim[i].GetComponent<Renderer>();
        }

        enemiesToSpawnText.text = enemiesToSpawn.ToString();

        spawnPoint = spawnPoints[currentSpawnPoint];

        StartCoroutine(SpawnEnemyProcessCoroutine());
    }
    private int GetDeadEnemyIndex(bool[] enemyAlive)
    {
        for (int i = 0; i < enemyAlive.Length; i++)
        {
            if (enemyAlive[i] == false)
            {
                return i;
            }
        }
        return -1;
    }

    public void SpawnEnemy()
    {

        if (iterateOverSpawnList < enemiesList.GetEnemiesListForLevel(GameLogic.levelNum).Length)
        {
            enemyIdToSpawn = enemiesList.GetEnemiesListForLevel(GameLogic.levelNum)[iterateOverSpawnList];
            if (enemyIdToSpawn < 0 || enemyIdToSpawn > 7)
            {
                enemyIdToSpawn = 0;
            }
            iterateOverSpawnList++;
        }

        GameObject newEnemy = Instantiate(GetPrefabTypeById(enemyIdToSpawn)[enemyIndexToSpawn], spawnPoint.position, Quaternion.identity);

        if (newEnemy.CompareTag("EnemyLvl1"))
        {
            EnemyLvl1 scriptEnemyLvl1 = newEnemy.GetComponent<EnemyLvl1>();
            scriptEnemyLvl1.hasPowerup = EnemyHasPowerup(enemyIdToSpawn);
        }
        else if (newEnemy.CompareTag("EnemyLvl2"))
        {
            EnemyLvl2 scriptEnemyLvl2 = newEnemy.GetComponent<EnemyLvl2>();
            scriptEnemyLvl2.hasPowerup = EnemyHasPowerup(enemyIdToSpawn);
        }
        else if (newEnemy.CompareTag("EnemyLvl3"))
        {
            EnemyLvl3 scriptEnemyLvl3 = newEnemy.GetComponent<EnemyLvl3>();
            scriptEnemyLvl3.hasPowerup = EnemyHasPowerup(enemyIdToSpawn);
        }
        else if (newEnemy.CompareTag("EnemyLvl4"))
        {
            EnemyLvl4 scriptEnemyLvl4 = newEnemy.GetComponent<EnemyLvl4>();
            scriptEnemyLvl4.hasPowerup = EnemyHasPowerup(enemyIdToSpawn);
        }

        enemiesToSpawn--;

        enemyAlive[enemyIndexToSpawn] = true;
        enemiesToSpawnText.text = enemiesToSpawn.ToString();

    }

    private IEnumerator SpawnEnemyProcessCoroutine()
    {
        spawnEnemyProcessIsInProgress = true;

        float checkRadius = 1.0f;

        enemyIndexToSpawn = GetDeadEnemyIndex(enemyAlive);

        while (!spawnPointHasEnoughRoom)
        {
                
            spawnPoint = spawnPoints[currentSpawnPoint];

            StartCoroutine(ShowPreSpawnAnimation(currentSpawnPoint));

            yield return new WaitForSeconds(1f);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, checkRadius, obstructionMask);

            if (colliders.Length == 0)
            {
                spawnPointHasEnoughRoom = true;
            }

            currentSpawnPoint++;

            if (currentSpawnPoint > 2)
            {
                currentSpawnPoint = 0;
            }

        }

        SpawnEnemy();

        spawnPointHasEnoughRoom = false;

        yield return new WaitForSeconds(cooldownToSpawn);

        spawnEnemyProcessIsInProgress = false;

        if (GetDeadEnemyIndex(enemyAlive) != -1 && enemiesToSpawn > 0)
        {
            StartCoroutine(SpawnEnemyProcessCoroutine());
        }

    }

    private bool EnemyHasPowerup(int enemyIdToSpawnLocal)
    {
        if (enemyIdToSpawnLocal % 2 != 0)
        {
            return true;
        }
        else if (enemyIdToSpawnLocal % 2 == 0)
        {
            return false;
        }
        return false;
    }

    private IEnumerator ShowPreSpawnAnimation(int spawnPointIndex)
    {
        spawnAnimationRenderer[spawnPointIndex].enabled = true;

        yield return new WaitForSeconds(1f);

        spawnAnimationRenderer[spawnPointIndex].enabled = false;

    }
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

    bool AllEnemiesDead()
    {
        foreach (bool alive in enemyAlive)
        {
            if (alive) return false;
        }
        return true;

    }

    private void OnEnable()
    {
        EnemyLvl1.OnDestroyed += OnObjectDestroyed;
        EnemyLvl2.OnDestroyed += OnObjectDestroyed;
        EnemyLvl3.OnDestroyed += OnObjectDestroyed;
        EnemyLvl4.OnDestroyed += OnObjectDestroyed;
    }

    private void OnDisable()
    {
        EnemyLvl1.OnDestroyed -= OnObjectDestroyed;
        EnemyLvl2.OnDestroyed -= OnObjectDestroyed;
        EnemyLvl3.OnDestroyed -= OnObjectDestroyed;
        EnemyLvl4.OnDestroyed -= OnObjectDestroyed;
    }

    private void OnObjectDestroyed(Enemy obj)
    {
        if (enemiesToSpawn > 0)
        {
            if (spawnEnemyProcessIsInProgress)
            {
                return;
            }
            if (GameLogic.GameOver)
            {
                return;
            }
            StartCoroutine(SpawnEnemyProcessCoroutine());
        }
        else if (AllEnemiesDead())
        {
            OnLevelFinished();
        }
    }

    void OnLevelFinished()
    {
        if (loadingNextLevelIsInProgress)
        {
            return;
        }
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        loadingNextLevelIsInProgress = true; 

        yield return new WaitForSeconds(1f);

        if (GameLogic.levelNum <= GameLogic.finalLevelNum)
        {
            GameLogic.levelNum++;
            Debug.Log("GameLogic.levelNum = " + GameLogic.levelNum);
            SceneManager.LoadScene("Scoreboard");
        }

    }

}

/*
3 static spawn points, but 4 enemies with unique indices
if the enemy with corresponding index dies the new one gets to spawn

theres an x second cooldown before the enemy spawns
this cooldown is shared between all the to-be-spawned enemies
 */