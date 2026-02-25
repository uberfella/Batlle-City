using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [SerializeField] private RectTransform pauseText;
    [SerializeField] private Vector2 pausedPosition;
    [SerializeField] private Vector2 hiddenPosition;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        //pauseText.SetActive(true);
        pauseText.anchoredPosition = pausedPosition;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        //pauseText.SetActive(false);
        pauseText.anchoredPosition = hiddenPosition;
    }
}