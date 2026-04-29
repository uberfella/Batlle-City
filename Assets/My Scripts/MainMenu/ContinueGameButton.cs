using System.Collections;
using TMPro;
using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    public GameObject continueButton;

    void Start()
    {
        if (!PlayerPrefs.HasKey("HasSaveData"))
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }
}
