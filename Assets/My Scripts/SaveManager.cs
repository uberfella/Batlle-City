using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    private void Start()
    {

    }

    void Update()
    {
        
    }

    public static void SaveGame()
    {
        //PlayerPrefs doesnt support booleans so we use some awkward conversions
        int highScoreHasBeenBeatenInt = ScoreCount.highScoreHasBeenBeaten ? 1 : 0;
        //0 1 2                <= 3
        if (GameLogic.levelNum <= GameLogic.finalLevelNum)
        {
            PlayerPrefs.SetInt("SavedLevel", GameLogic.levelNum);
            //Debug.Log("saving GameLogic.levelNum as " + GameLogic.levelNum);
        }
        PlayerPrefs.SetInt("PlayerLives", PlayerProperties.playerLives);
        PlayerPrefs.SetInt("PlayerUpgrade", PlayerProperties.playerLevel);
        PlayerPrefs.SetInt("HighScoreHasBeenBeaten", highScoreHasBeenBeatenInt);
        //Debug.Log("Saving HighScoreHasBeenBeaten as "+ highScoreHasBeenBeatenInt);
        PlayerPrefs.SetInt("HasSaveData", 1);
        PlayerPrefs.Save();
        //Debug.Log("Game saved");
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            //Debug.Log("restoring GameLogic.levelNum as " + GameLogic.levelNum); 
            GameLogic.levelNum = PlayerPrefs.GetInt("SavedLevel");
            PlayerProperties.playerLives = PlayerPrefs.GetInt("PlayerLives");
            PlayerProperties.playerLevel = PlayerPrefs.GetInt("PlayerUpgrade");
            ScoreCount.highScoreHasBeenBeaten = PlayerPrefs.GetInt("HighScoreHasBeenBeaten") != 0;
            //Debug.Log("Restoring HighScoreHasBeenBeaten as " + PlayerPrefs.GetInt("HighScoreHasBeenBeaten") + " which translates to " + ScoreCount.highScoreHasBeenBeaten + " as a respected boolean");


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
                //load the end screen
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
        
        //Debug.Log("Game erased");
    }

}
