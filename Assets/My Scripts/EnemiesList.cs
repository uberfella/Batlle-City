using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    int[,] numbers = { { 1, 4, 2 }, { 3, 6, 8 } };
    //{0, 0, 0, 0, 0, 1, 1, 0};
    private int[] level1 = { 1, 0, 0, 0, 0, 1, 1, 0 };
    private int[] level2 = { 0, 0, 0, 0, 0, 1, 1, 0 };
    public int foo = 1;

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
