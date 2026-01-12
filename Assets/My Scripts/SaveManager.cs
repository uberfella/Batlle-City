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

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("SavedLevel", BootstrappedData.levelNum);
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
            BootstrappedData.levelNum = PlayerPrefs.GetInt("SavedLevel");
            PlayerSpawner.playerLives = PlayerPrefs.GetInt("PlayerLives");
            PlayerController2D.playerLevel = PlayerPrefs.GetInt("PlayerUpgrade");

            string sceneName = "Level" + BootstrappedData.levelNum;
            SceneManager.LoadScene(sceneName);
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
