using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    public GameObject continueButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
