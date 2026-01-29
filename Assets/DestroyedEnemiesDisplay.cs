using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enemy;

public class DestroyedEnemiesDisplay : MonoBehaviour
{
    public Text[] enemyCountTexts;   // Size 4 in Inspector
    public Text[] enemyPointTexts;   // If needed
    public Text TotalCount;
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
    }


    void Update()
    {
        
    }
}
