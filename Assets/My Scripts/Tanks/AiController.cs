using UnityEngine;

public class AiController : MonoBehaviour
{
    //private int direction;

    public int horizontalInput;
    public int verticalInput;

    private int minValForInput = -1;
    private int maxValForInput = 2; //actually equals 1
    private int minValForShootCd = 1;
    private int maxValForShootCd = 3; //actually equals 2
    //private int minValForC = 1;
    //private int maxValForShootCd = 3; //actually equals 2

    public int GetHorizontalRandom() 
    {
        int random = Random.Range(minValForInput, maxValForInput);
        return random;
    }
        
    public int GetVerticalRandom()
    {
        int random = Random.Range(minValForInput, maxValForInput);
        return random;
    }

    public int GetShootCooldown() 
    {
        int random = Random.Range(minValForShootCd, maxValForShootCd);
        return random;
    }

}
