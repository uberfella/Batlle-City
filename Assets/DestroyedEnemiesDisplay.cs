using UnityEngine;
using UnityEngine.UI;

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
        //currentScoreText.text = currentScore.ToString("D6");
        gameLogic = FindFirstObjectByType<GameLogic>();
        //EnemyLvl1Count.text = gameLogic.destroyedByType["EnemyLvl1"].ToString("D6");
    }


    void Update()
    {
        
    }
}
