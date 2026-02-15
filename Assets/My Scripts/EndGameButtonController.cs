using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButtonController : MonoBehaviour
{

    void Start()
    {
        

    }

    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
