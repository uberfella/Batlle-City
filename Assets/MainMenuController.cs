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
    }

    public void TryStartGame()
    {
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
        SaveManager.LoadGame();
    }

    public void ConfirmYes()
    {
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
        PlayerProperties.playerLevel = 0;
        SceneManager.LoadScene("Level0");
    }
}
