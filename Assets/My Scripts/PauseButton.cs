using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private RectTransform pauseText;
    [SerializeField] private Vector2 pausedPosition;
    [SerializeField] private Vector2 hiddenPosition;
    void Update()
    {
        if (GameLogic.GameOver)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause))
        {
            if (PauseManager.Instance.isPaused)
            {
                PauseManager.Instance.Resume();
                //pauseText.anchoredPosition = pausedPosition;
                pauseText.anchoredPosition = hiddenPosition;
            }
            else
            {
                Debug.Log("Hi");
                PauseManager.Instance.Pause();
                //pauseText.anchoredPosition = hiddenPosition;
                pauseText.anchoredPosition = pausedPosition;

            }
        }
    }
}
