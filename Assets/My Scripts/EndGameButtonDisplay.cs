using System.Collections;
using UnityEngine;

public class EndGameButtonDisplay : MonoBehaviour
{
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

        yield return new WaitForSeconds(11.0f);

        // SHOW BUTTON
        buttonsRow.SetActive(true);
        iconSelector.SetActive(true);

    }
}
