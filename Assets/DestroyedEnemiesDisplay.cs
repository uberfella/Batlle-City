using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enemy;

public class DestroyedEnemiesDisplay : MonoBehaviour
{
    public GameObject[] rows;
    public Text[] enemyCountTexts;   // Size 4 in Inspector
    public Text[] enemyPointTexts;   // If needed
    public Text TotalCount;
    public GameObject totalRow;
    public GameObject buttonsRow;
    public GameObject iconSelector;
    public float delayBetweenRows = 0.4f;
    public bool scoreBoardHasFinishedDrawing = false;
    //public float countDuration = 0.8f;
    private int totalCountValue = 0;
    private MenuSelectorScoreboard menuSelectorScoreboard;
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
        //int grandTotal = 0;

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].SetActive(true);

            EnemyType type = (EnemyType)i;
            if (GameLogic.Instance.destroyedByType.TryGetValue(type, out int a))
            {
                int count = GameLogic.Instance.destroyedByType[type];
                int points = ((i + 1) * 100 * count);
                yield return StartCoroutine(AnimateNumber(enemyCountTexts[i], count, 1));
                yield return StartCoroutine(AnimateNumber(enemyPointTexts[i], points, 100));

            }

            //int points = count * GameLogic.Instance.pointsPerType[type];



            //grandTotal += points;

            yield return new WaitForSeconds(delayBetweenRows);
        }

        // TOTAL
        totalRow.SetActive(true);
        //yield return StartCoroutine(AnimateNumber(TotalCount, totalCountValue));

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

            yield return new WaitForSeconds(0.2f);
        }

        text.text = targetValue.ToString();
    }
}
