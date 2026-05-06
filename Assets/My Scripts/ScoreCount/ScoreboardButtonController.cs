using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardButtonController : MonoBehaviour
{
    public void LoadNextLevel()
    {
        GameLogic.Instance.destroyedByType.Clear();
        //0 1 2 3              < 3
        if (GameLogic.levelNum < GameLogic.finalLevelNum)
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

    //is referenced in onClick() -> Scoreboard scene -> HOME button
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
