using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        BootstrappedData.levelNum = 0;
        SceneManager.LoadScene("Level0");
    }

    public void ContinueGame()
    {
        SaveManager.LoadGame(); // wherever LoadGame is defined
    }
}
