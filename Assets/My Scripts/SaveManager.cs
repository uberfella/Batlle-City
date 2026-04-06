using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static void SaveGame()
    {
        //PlayerPrefs doesnt support booleans so we use some awkward conversions
        int highScoreHasBeenBeatenInt = ScoreCount.highScoreHasBeenBeaten ? 1 : 0;
        //0 1 2                <= 3
        if (GameLogic.levelNum <= GameLogic.finalLevelNum)
        {
            PlayerPrefs.SetInt("SavedLevel", GameLogic.levelNum);
        }
        PlayerPrefs.SetInt("PlayerLives", PlayerProperties.playerLives);
        PlayerPrefs.SetInt("PlayerUpgrade", PlayerProperties.playerLevel);
        PlayerPrefs.SetInt("HighScoreHasBeenBeaten", highScoreHasBeenBeatenInt);
        PlayerPrefs.SetInt("HasSaveData", 1);
        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            GameLogic.levelNum = PlayerPrefs.GetInt("SavedLevel");
            PlayerProperties.playerLives = PlayerPrefs.GetInt("PlayerLives");
            PlayerProperties.playerLevel = PlayerPrefs.GetInt("PlayerUpgrade");
            ScoreCount.highScoreHasBeenBeaten = PlayerPrefs.GetInt("HighScoreHasBeenBeaten") != 0;

            //loading the game
            if (GameLogic.levelNum < GameLogic.finalLevelNum)
            {
                string sceneName = "Level" + GameLogic.levelNum;
                SceneManager.LoadScene(sceneName);
            }
            else
            //if the player completes the last level, goes to main menu and continues the game later
            {
                //delete the save — the games already beaten
                EraseSave();
                SceneManager.LoadScene("End Scene");
            }

        }
    }

    public static void EraseSave()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        PlayerPrefs.DeleteKey("PlayerLives");
        PlayerPrefs.DeleteKey("PlayerUpgrade");
        PlayerPrefs.DeleteKey("HasSaveData");
    }

}
