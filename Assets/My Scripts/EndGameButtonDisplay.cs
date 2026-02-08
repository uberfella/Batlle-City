using System.Collections;
using UnityEngine;

public class EndGameButtonDisplay : MonoBehaviour
{
    public GameObject buttonsRow;
    public GameObject iconSelector;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowHomeButton());
    }

    // Update is called once per frame
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
