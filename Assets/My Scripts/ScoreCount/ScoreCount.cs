using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public static int currentScore = 0;
    public static int highScore = 0;
    public static bool highScoreHasBeenBeaten = true;
    public Text currentScoreText;
    public Text highScoreText; //display hiscore on levels 
    public TextMeshProUGUI highScoreTextEnd; //display hiscore on end screen 

    void Awake()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = highScore.ToString("D6");
        if (IsScene("End Scene"))
        {
            highScoreTextEnd.text = highScore.ToString("D6");
        }
        if (IsScene("Scoreboard"))
        {
            UpdateHighScore();
        }
        UpdateCurrentScore();
    }

    bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }

    public void UpdateCurrentScore()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = currentScore.ToString("D6");
        }
    }

    public void UpdateHighScore() 
    {
        if (currentScore > highScore)
        {
            highScoreHasBeenBeaten = true;
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
        UpdateCurrentScore();
    }

    private void AddScore(int amount)
    {
        currentScore += amount;
    }
}
