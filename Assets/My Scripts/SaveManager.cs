using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator TriggerLevelEndAndSaveGame()
    //{
    //    if ((levelNum < finalLevelNum))
    //    {
    //        float elapsed = 0f;

    //    }
    //}

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("SavedLevel", GameLogic.levelNum);
        PlayerPrefs.SetInt("PlayerLives", PlayerSpawner.playerLives);
        PlayerPrefs.SetInt("PlayerUpgrade", PlayerController2D.playerLevel);
        PlayerPrefs.SetInt("HasSaveData", 1);
        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            GameLogic.levelNum = PlayerPrefs.GetInt("SavedLevel");
            PlayerSpawner.playerLives = PlayerPrefs.GetInt("PlayerLives");
            PlayerController2D.playerLevel = PlayerPrefs.GetInt("PlayerUpgrade");

            string sceneName = "Level" + GameLogic.levelNum;
            SceneManager.LoadScene(sceneName);
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
