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

    void Start()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = highScore.ToString("D6");
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = currentScore.ToString("D6");
    }

    public void UpdateHighScore() 
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("Highscore", currentScore);
            PlayerPrefs.Save();
        }
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
    }

    private void AddScore(int amount)
    {
        currentScore += amount;
    }
}
