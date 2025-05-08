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
    public bool[] enemyAlive = new bool[4] { false, false, false, false }; //7, 10, 11, 12
    public LayerMask obstructionMask;
    public EnemiesList enemiesList;

    private float timer = 0f;
    private float cooldownToSpawn = 5f;
    private int iterateOverSpawnList = 0;
    private int enemyIdToSpawn = 0;
    private GameObject[] spawnAnim;
    private Renderer[] spawnAnimationRenderer;

    Animator animator;

    void Start()
    {

        //GameLogic.levelNum = 0;
        enemiesList = FindFirstObjectByType< EnemiesList >();
        //currentArray = enemiesList.GetEnemiesListForLevel(GameLogic.levelNum);
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
                    spawnAnimationRenderer[i].enabled = true;
                }
                if (timer >= cooldownToSpawn)
                {
                    SpawnEnemy(i);
                    spawnAnimationRenderer[i].enabled = false;
                    enemiesToSpawn--;
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
            int randomSpawnPoint = Random.Range(0, 3);
            spawnPoint = spawnPoints[randomSpawnPoint];
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, checkRadius, obstructionMask);

        if (colliders.Length == 0 && iterateOverSpawnList < enemiesList.GetEnemiesListForLevel(GameLogic.levelNum).Length)  // No obstructions
        {
            enemyIdToSpawn = enemiesList.GetEnemiesListForLevel(GameLogic.levelNum)[iterateOverSpawnList]; //0 1 2 3 4 
            iterateOverSpawnList++;
            GameObject newEnemy = Instantiate(GetPrefabTypeById(enemyIdToSpawn)[index], spawnPoint.position, Quaternion.identity);
            enemyAlive[index] = true;
        }
        else
        {
            //Debug.Log("Spawn point is obstructed. Try again later.");
        }
    }

    //int random = Random.Range(minValForInput, maxValForInput);
    //spawnPoint[3]

    private GameObject[] GetPrefabTypeById(int id)
    {
        switch (id)
        {
            case 0:
                return enemyPrefabLvl1;
            case 1:
                return enemyPrefabLvl2;
            case 2:
                return enemyPrefabLvl3;
            case 3:
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