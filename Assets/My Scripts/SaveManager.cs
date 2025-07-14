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
        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            GameLogic.levelNum = PlayerPrefs.GetInt("SavedLevel");
            PlayerSpawner.playerLives = PlayerPrefs.GetInt("PlayerLives");

            string sceneName = "Level" + GameLogic.levelNum;
            SceneManager.LoadScene(sceneName);
        }
    }

    public static void EraseSave()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        PlayerPrefs.DeleteKey("PlayerLives");
    }

}
