using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public GameObject nextLevelButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!GameLogic.GameOver)
        {
            nextLevelButton.SetActive(true);
        }
        else
        {
            nextLevelButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
