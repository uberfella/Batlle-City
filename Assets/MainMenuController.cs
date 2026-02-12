using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        GameLogic.GameOver = false;
        GameLogic.Instance.destroyedByType.Clear();
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        GameLogic.levelNum = 0;
        ScoreCount.currentScore = 0;
        ScoreCount.highScoreHasBeenBeaten = false;
        PlayerSpawner.playerLives = 1;
        SceneManager.LoadScene("Level0");
    }

    public void ContinueGame()
    {
        SaveManager.LoadGame(); // wherever LoadGame is defined
    }
}
