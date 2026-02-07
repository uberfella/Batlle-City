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
        //0 1 2                <= 3
        if (GameLogic.levelNum <= GameLogic.finalLevelNum)
        {
            PlayerPrefs.SetInt("SavedLevel", GameLogic.levelNum + 1);
        }
        PlayerPrefs.SetInt("PlayerLives", PlayerSpawner.playerLives);
        PlayerPrefs.SetInt("PlayerUpgrade", PlayerController2D.playerLevel);
        PlayerPrefs.SetInt("HasSaveData", 1);
        PlayerPrefs.Save();
        Debug.Log("Game saved");
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            GameLogic.levelNum = PlayerPrefs.GetInt("SavedLevel");
            PlayerSpawner.playerLives = PlayerPrefs.GetInt("PlayerLives");
            PlayerController2D.playerLevel = PlayerPrefs.GetInt("PlayerUpgrade");

            if (GameLogic.levelNum < GameLogic.finalLevelNum)
            {
                string sceneName = "Level" + GameLogic.levelNum;
                SceneManager.LoadScene(sceneName);
            }
            else
            {
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
        
        Debug.Log("Game erased");
    }

}
