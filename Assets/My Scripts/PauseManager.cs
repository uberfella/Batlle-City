using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Pause()
    {
        Debug.Log("isPaused = true;");

        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("isPaused = false;");

        isPaused = false;
        Time.timeScale = 1f;
    }
}