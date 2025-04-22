using UnityEngine;

public class AiController : MonoBehaviour
{
    public int horizontalInput;
    public int verticalInput;

    private int minValForInput = 0;
    private int maxValForInput = 4; //actually equals 3
    private int minValForShootCd = 1;
    private int maxValForShootCd = 3; //actually equals 2

    public int GetHorizontalVerticalInput() 
    {
        //possible values for vertical and horizontal inputs can be
        //gotta implement it as predefined combinations, so we can exclude diagonal movement as well
        //1,0 -1,0 0,1 0,-1
        //0    1   2   3
        int random = Random.Range(minValForInput, maxValForInput);
        return random;
    }

    public int GetShootCooldown() 
    {
        int random = Random.Range(minValForShootCd, maxValForShootCd);
        return random;
    }

}
