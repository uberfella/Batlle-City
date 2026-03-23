using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public GameObject confirmPanel;

    void Start()
    {
        GameLogic.GameOver = false;
        GameLogic.Instance.destroyedByType.Clear();
        Debug.Log("Has saved data " + PlayerPrefs.HasKey("HasSaveData"));
    }

    public void TryStartGame()
    {
        //GameLogic.levelNum = 0;
        //ScoreCount.currentScore = 0;
        //ScoreCount.highScoreHasBeenBeaten = false;
        //PlayerSpawner.playerLives = 2;
        //SceneManager.LoadScene("Level0");
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            confirmPanel.SetActive(true);
        }
        else
        {
            StartGame();
        }
    }

    public void ContinueGame()
    {
        SaveManager.LoadGame(); // wherever LoadGame is defined
    }

    public void ConfirmYes()
    {
        //confirmPanel.SetActive(false);
        StartGame();
    }

    public void ConfirmNo()
    {
        confirmPanel.SetActive(false);
    }

    private void StartGame()
    {
        GameLogic.levelNum = 0;
        ScoreCount.currentScore = 0;
        ScoreCount.highScoreHasBeenBeaten = false;
        PlayerProperties.playerLives = 2;

        SceneManager.LoadScene("Level0");
    }
}
