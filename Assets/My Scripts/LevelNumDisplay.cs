using UnityEngine;
using UnityEngine.UI;

public class LevelNumDisplay : MonoBehaviour
{
    public Text levelNumText;
    public Text currentScoreText;
    public Text highScoreText;
    void Start()
    {
        levelNumText.text = (GameLogic.levelNum).ToString();
        currentScoreText.text = ScoreCount.currentScore.ToString("D6");
        highScoreText.text = ScoreCount.highScore.ToString("D6");
    }
}
