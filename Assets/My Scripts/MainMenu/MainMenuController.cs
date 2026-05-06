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

            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

            confirmPanel.SetActive(true);
        }
        else
        {
            StartGame();
        }
    }

    public void ContinueGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

        SaveManager.LoadGame();
    }

    public void ConfirmYes()
    {

        StartGame();
    }

    public void ConfirmNo()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

        confirmPanel.SetActive(false);
    }

    private void StartGame()
    {

        AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

        GameLogic.levelNum = 0;
        Debug.Log("GameLogic.levelNum = " + GameLogic.levelNum);

        ScoreCount.currentScore = 0;
        ScoreCount.highScoreHasBeenBeaten = false;
        PlayerProperties.playerLives = 2;
        PlayerProperties.playerLevel = 0;
        SceneManager.LoadScene("Level0");
    }
}
