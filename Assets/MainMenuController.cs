using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameLogic gameLogic;
    void Start()
    {
        GameLogic.GameOver = false;
        gameLogic = FindFirstObjectByType<GameLogic>();
        gameLogic.destroyedByType.Clear();
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        GameLogic.levelNum = 0;
        ScoreCount.currentScore = 0;
        PlayerSpawner.playerLives = 1;
        SceneManager.LoadScene("Level0");
    }

    public void ContinueGame()
    {
        SaveManager.LoadGame(); // wherever LoadGame is defined
    }
}
