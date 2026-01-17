using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameLogic.GameOver = false;
        Debug.Log("GameOver = " + GameLogic.GameOver);
    }

    // Update is called once per frame
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
