using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enemy;

public class DestroyedEnemiesDisplay : MonoBehaviour
{
    public Text EnemyLvl1Points;
    public Text EnemyLvl2Points;
    public Text EnemyLvl3Points;
    public Text EnemyLvl4Points;
    public Text EnemyLvl1Count;
    public Text EnemyLvl2Count;
    public Text EnemyLvl3Count;
    public Text EnemyLvl4Count;
    public Text TotalCount;
    private GameLogic gameLogic;
    void Start()
    {
        //destroyedByType[type]
        //public Dictionary<EnemyType, int> destroyedByType = new();
        //currentScoreText.text = currentScore.ToString("D6");
        gameLogic = FindFirstObjectByType<GameLogic>();
        //int count = gameLogic.destroyedByType["EnemyLvl1"];

        EnemyLvl1Count.text = gameLogic.destroyedByType[EnemyType.EnemyLvl1].ToString();

        

        //if (gameLogic.destroyedByType.TryGetValue(EnemyType.EnemyLvl1, out int enemyLvl1Kills))
        //{
        //    Debug.Log("enemyLvl1 killed: " + enemyLvl1Kills);
        //}
    }


    void Update()
    {
        
    }
}
