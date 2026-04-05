using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Enemy;

public class DestroyedEnemiesDisplay : MonoBehaviour
{
    public GameObject[] rows;
    public Text[] enemyCountTexts;
    public Text[] enemyPointTexts;
    public Text TotalCount;
    public GameObject totalRow;
    public GameObject buttonsRow;
    public GameObject iconSelector;
    public float delayBetweenRows = 0.4f;
    public bool scoreBoardHasFinishedDrawing = false;

    private int totalCountValue = 0;
    private MenuSelectorScoreboard menuSelectorScoreboard;
    private float scoreCountingSpeed = 0.15f;

    void Start()
    {
        menuSelectorScoreboard = FindFirstObjectByType<MenuSelectorScoreboard>();

        for (int i = 0; i < 4; i++)
        {
            EnemyType type = (EnemyType)i;
             
            //if the dictionary holds an enemy then type display the value, otherwise display 0
            if (GameLogic.Instance.destroyedByType.TryGetValue(type, out int a)) 
            {
                int count = GameLogic.Instance.destroyedByType[type];
                enemyCountTexts[i].text = 0.ToString();
                enemyPointTexts[i].text = 0.ToString();
                totalCountValue += count;
            }
            TotalCount.text = totalCountValue.ToString();
        }

        StartCoroutine(ShowScoreboard());
    }

    IEnumerator ShowScoreboard()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].SetActive(true);

            EnemyType type = (EnemyType)i;
            if (GameLogic.Instance.destroyedByType.TryGetValue(type, out int a))
            {
                int count = GameLogic.Instance.destroyedByType[type];
                int points = ((i + 1) * 100 * count);
                StartCoroutine(AnimateNumber(enemyCountTexts[i], count, 1));
                yield return StartCoroutine(AnimateNumber(enemyPointTexts[i], points, 100));

            }

            yield return new WaitForSeconds(delayBetweenRows);
        }

        // TOTAL
        totalRow.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // SHOW BUTTONS
        buttonsRow.SetActive(true);
        iconSelector.SetActive(true);
        // and move SelectorIconToTheButton
        scoreBoardHasFinishedDrawing = true;
        menuSelectorScoreboard.MoveSelector();
    }

    IEnumerator AnimateNumber(Text text, int targetValue, int progressStep)
    {
        int startValue = 0;

        while (startValue < targetValue)
        {
            startValue += progressStep;

            if (startValue > targetValue)
                startValue = targetValue;

            text.text = startValue.ToString();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.scoreCountingSound);

            yield return new WaitForSeconds(scoreCountingSpeed);

        }

        text.text = targetValue.ToString();
    }
}
