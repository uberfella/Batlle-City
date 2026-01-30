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
    public float delayBetweenRows = 0.4f;
    public float countDuration = 0.8f;
    private int totalCountValue = 0;
    void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            EnemyType type = (EnemyType)i;
             
            //if the dictionary holds an enemy then type display the value, otherwise display 0
            if (GameLogic.Instance.destroyedByType.TryGetValue(type, out int a)) 
            {
                int count = GameLogic.Instance.destroyedByType[type];
                enemyCountTexts[i].text = count.ToString();
                enemyPointTexts[i].text = ((i + 1) * 100 * count).ToString();
                totalCountValue += count;
            }
            // (0 + 1) * 100 * x = 100
            // (1 + 1) * 100 * x = 200
            // (2 + 1) * 100 * x = 300
            // (3 + 1) * 100 * x = 400
            TotalCount.text = totalCountValue.ToString();
        }

        StartCoroutine(ShowScoreboard());
    }

    IEnumerator ShowScoreboard()
    {
        int grandTotal = 0;

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].SetActive(true);

            EnemyType type = (EnemyType)i;
            int count = GameLogic.Instance.destroyedByType[type];
            //int points = count * GameLogic.Instance.pointsPerType[type];

            //yield return StartCoroutine(AnimateNumber(countTexts[i], count));
            //yield return StartCoroutine(AnimateNumber(pointTexts[i], points));

            //grandTotal += points;

            yield return new WaitForSeconds(delayBetweenRows);
        }

        // TOTAL
        totalRow.SetActive(true);
        //yield return StartCoroutine(AnimateNumber(totalText, grandTotal));

        yield return new WaitForSeconds(0.5f);

        // SHOW BUTTONS
        buttonsRow.SetActive(true);
    }

    IEnumerator AnimateNumber(Text text, int targetValue)
    {
        float timer = 0f;
        int startValue = 0;

        while (timer < countDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / countDuration;

            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, progress));
            text.text = currentValue.ToString();

            yield return null;
        }

        text.text = targetValue.ToString();
    }


    void Update()
    {
        
    }
}
