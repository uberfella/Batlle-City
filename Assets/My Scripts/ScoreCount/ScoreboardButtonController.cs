using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardButtonController : MonoBehaviour
{
    void Start()
    {

    }

    public void LoadNextLevel()
    {
        GameLogic.Instance.destroyedByType.Clear();
        if (GameLogic.levelNum <= GameLogic.finalLevelNum)
        {
            string sceneName = "Level" + GameLogic.levelNum;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SaveManager.EraseSave();
            SceneManager.LoadScene("End Scene");
        }
    }

    public void ReturnToMainMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

}
