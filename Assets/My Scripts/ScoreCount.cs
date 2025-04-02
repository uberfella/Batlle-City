using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static int currentScore = 0;
    public static int highScore = 0;
    public Text currentScoreText;
    public Text highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = currentScore.ToString("D6");
        highScoreText.text = highScore.ToString("D6");
    }
}
