using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static int currentScore = 0;
    public static int highScore = 0;
    public Text currentScoreText;
    public Text highScoreText;

    //private EnemyLvl1 enemyLvl1;
    //private EnemyLvl2 enemyLvl2;
    //private EnemyLvl3 enemyLvl3;
    //private EnemyLvl4 enemyLvl4;

    void Start()
    {
        //enemyLvl1 = GetComponent<EnemyLvl1>();
        //enemyLvl2 = GetComponent<EnemyLvl2>();
        //enemyLvl3 = GetComponent<EnemyLvl3>();
        //enemyLvl4 = GetComponent<EnemyLvl4>();
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = currentScore.ToString("D6");
        highScoreText.text = highScore.ToString("D6");
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
        AddScore(obj.scoreOnDestroy);
        //Debug.Log($"Destroyed: {obj.name}, ScoreValue: {obj.scoreOnDestroy}");
    }

    private void AddScore(int amount)
    {
        currentScore += amount;
    }
}
