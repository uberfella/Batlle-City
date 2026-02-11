using System.Collections;
using UnityEngine;

public class EndGameButtonDisplay : MonoBehaviour
{
    //public static int currentScore = 0;
    //public static int highScore = 0;
    //hiscore 
    public GameObject buttonsRow;
    public GameObject iconSelector;

    void Start()
    {


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
