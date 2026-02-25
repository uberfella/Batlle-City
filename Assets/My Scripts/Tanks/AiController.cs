using UnityEngine;

public class AiController : MonoBehaviour
{
    public int horizontalInput;
    public int verticalInput;

    private int minValForInput = 0;
    private int maxValForInput = 4; //actually equals 3
    private float minValForShootCd = 1.0f;
    private float maxValForShootCd = 2.0f;

    public int GetHorizontalVerticalInput() 
    {
        //possible values for vertical and horizontal inputs can be
        //gotta implement it as predefined combinations, so we can exclude diagonal movement as well
        //1,0 -1,0 0,1 0,-1
        //0    1   2   3
        int random = Random.Range(minValForInput, maxValForInput);
        return random;
    }

    public float GetShootCooldown() 
    {
        float random = Random.Range(minValForShootCd, maxValForShootCd);
        return random;
    }
}
