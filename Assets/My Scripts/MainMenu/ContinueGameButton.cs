using System.Collections;
using TMPro;
using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    public GameObject continueButton;

    private MainMenuController mainMenuController;

    void Start()
    {
        if (!PlayerPrefs.HasKey("HasSaveData"))
        {
            continueButton.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowButton());
        }

        mainMenuController = FindFirstObjectByType<MainMenuController>();

    }

    IEnumerator ShowButton()
    {
        //Debug.Log("waiting " + mainMenuController.moveDuration + " seconds");
        yield return new WaitForSeconds(mainMenuController.moveDuration);
        //Debug.Log("wait is over");
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            continueButton.SetActive(true);
        }
        else
        {
        }
    }



}
