using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesList : MonoBehaviour
{
    //0 enemyLvl1 regular
    //1 enemyLvl1 powerup
    //2 enemyLvl2 regular
    //3 enemyLvl2 powerup
    //4 enemyLvl3 regular
    //5 enemyLvl3 powerup
    //6 enemyLvl4 regular
    //7 enemyLvl4 powerup

    //private int[] level1 = { 2, 3, 2, 3, 0, 1, 3, 4, 6, 6, 7, 0, 1, 1, 1, 2, 3, 0, 1, 1 };
    private int[][] levels =
    {
        new int[] { 1, 7, 5, 7, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 4, 5 }, // level 0
        new int[] { 0, 2, 2, 0, 0, 1, 1, 0, 1, 2, 3, 0, 1, 1, 1, 2, 3, 0, 1, 1 }, // level 1
        new int[] { 0, 2, 2, 0, 0, 1, 1, 0, 1, 2, 3, 0, 1, 1, 1, 2, 3, 0, 1, 1 }  // level 2
    };

    public int[] GetEnemiesListForLevel(int level)
    {
        if (level < 0 || level >= levels.Length)
        {
            Debug.LogWarning("Level index out of range, defaulting to 0");
            return levels[0];
        }

        return levels[level];
    }

}
