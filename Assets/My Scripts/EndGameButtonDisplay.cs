using System.Collections;
using UnityEngine;

public class EndGameButtonDisplay : MonoBehaviour
{
    //public static int currentScore = 0;
    //public static int highScore = 0;
    //hiscore beaten - Game Over ImageText hide in N seconds 
    //hiscore not beaten - Game Over ImageText not hide, hide static_ScoreHigh and ScoreHighVal, show ButtonsRow and iconSelector in N seconds
    public GameObject buttonsRow;
    public GameObject iconSelector;
    //public GameObject gameOverImageText;
    public GameObject static_ScoreHigh;
    public GameObject scoreHighVal;

    void Start()
    {

        //static_ScoreHigh.SetActive(true);
        StartCoroutine(ShowHomeButton());
    }

    void Update()
    {
        
    }

    IEnumerator ShowHomeButton()
    {

        yield return new WaitForSeconds(9.0f);

        // SHOW BUTTON
        buttonsRow.SetActive(true);
        iconSelector.SetActive(true);

    }
}
