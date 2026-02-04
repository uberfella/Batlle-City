using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    private AudioManager audioManager;
    void Start()
    {
        GameLogic.GameOver = false;
        GameLogic.Instance.destroyedByType.Clear();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
