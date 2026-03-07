using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButtonController : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
