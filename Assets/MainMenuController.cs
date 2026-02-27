using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public GameObject GameTitle;
    public float moveDuration = 5.5f;
    public Vector2 targetPosition;
    private Vector2 startPosition;
    //-2.599976
    //161.3

    void Start()
    {


        GameLogic.GameOver = false;
        GameLogic.Instance.destroyedByType.Clear();
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        GameLogic.levelNum = 0;
        ScoreCount.currentScore = 0;
        ScoreCount.highScoreHasBeenBeaten = false;
        PlayerSpawner.playerLives = 2;
        SceneManager.LoadScene("Level0");
    }

    public void ContinueGame()
    {
        SaveManager.LoadGame(); // wherever LoadGame is defined
    }

    IEnumerator ShowMainMenu()
    {
        yield return StartCoroutine(PullMainMenuUp(startPosition, targetPosition, moveDuration));
    }

    IEnumerator PullMainMenuUp(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            GameTitle.transform.position = Vector2.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        //GameTitle.anchoredPosition = to;
    }
}
