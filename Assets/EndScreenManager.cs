using System.Collections;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    //[SerializeField] private SpriteRenderer spriteRenderer;
    //hiscore beaten - play GameOverJingle, hide Game Over ImageText in 2 seconds, show HISCORE underneath and play Ending theme
    //hiscore not beaten - dont hide Game Over ImageText, keep static_ScoreHigh and ScoreHighVal inactive
    public GameObject gameOverImageText;
    public GameObject static_ScoreHigh;
    public GameObject scoreHighVal;

    void Start()
    {

        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOverJingle);

        if (ScoreCount.highScoreHasBeenBeaten)
        {
            StartCoroutine(HideGameOverImageTextAndShowHiscoreTexts());
        }

    }

    IEnumerator HideGameOverImageTextAndShowHiscoreTexts()
    {
        yield return new WaitForSeconds(2.0f);
        gameOverImageText.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.endTheme);
        static_ScoreHigh.SetActive(true);
        scoreHighVal.SetActive(true);
    }
}
