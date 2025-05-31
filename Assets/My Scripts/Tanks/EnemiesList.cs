using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    //private int[] level1 = { 0, 0, 0, 0, 0, 1, 1, 0 };
    private int[] level1 = { 0, 1, 2, 3, 0, 1, 1, 0, 1, 2, 3, 0, 1, 1, 1, 2, 3, 0, 1, 1 };
    private int[] level2 = { 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 3, 0, 1, 1, 1, 2, 3, 0, 1, 1 };

    public int[] GetEnemiesListForLevel(int level)
    {
        switch (level)
        {
            case 0:
                return level1;
            case 1:
                return level2;
            default:
                return level1;
        }
    }

}
