using UnityEngine;
using UnityEngine.UI;

public class LevelNumDisplay : MonoBehaviour
{
    public Text levelNumText;
    public Text currentScoreText;
    public Text highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelNumText.text = (GameLogic.levelNum + 1).ToString();
        currentScoreText.text = ScoreCount.currentScore.ToString("D6");
        highScoreText.text = ScoreCount.highScore.ToString("D6");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
