using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private RectTransform pauseText;
    [SerializeField] private Vector2 pausedPosition;
    [SerializeField] private Vector2 hiddenPosition;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !GameLogic.GameOver)
        {
            if (PauseManager.Instance.isPaused)
            {
                PauseManager.Instance.Resume();
                pauseText.anchoredPosition = hiddenPosition;

            }
            else
            {
                PauseManager.Instance.Pause();
                pauseText.anchoredPosition = pausedPosition;
            }
        }
    }
}
