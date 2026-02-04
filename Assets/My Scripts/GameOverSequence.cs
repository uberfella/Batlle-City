using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSequence : MonoBehaviour
{
    public RectTransform gameOverText;

    public float moveDuration = 5.5f;
    public Vector2 targetPosition;
    private Vector2 startPosition;
    private Spawner spawner;
    private AudioManager audioManager;

    private void Awake()
    {

        spawner = FindFirstObjectByType<Spawner>();
    }
    void Start()
    {
        startPosition = gameOverText.anchoredPosition;
        gameOverText.gameObject.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {
        
    }

    public void TriggerGameOver()
    {
        if (!GameLogic.GameOver && !spawner.levelFinished)
        {
            audioManager.PlaySFX(audioManager.gameOverJingle);
            GameLogic.GameOver = true;
            Debug.Log("GameOver = " + GameLogic.GameOver);
            SaveManager.EraseSave();
            StartCoroutine(ShowGameOver());
        }
    }

    IEnumerator ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        yield return StartCoroutine(MoveText(startPosition, targetPosition, moveDuration));
        SceneManager.LoadScene("Scoreboard");
    }

    IEnumerator MoveText(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            gameOverText.anchoredPosition = Vector2.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameOverText.anchoredPosition = to; // Ensure it reaches the final position
    }
}
