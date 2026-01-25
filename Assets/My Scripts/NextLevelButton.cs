using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public GameObject nextLevelButton;

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

}
