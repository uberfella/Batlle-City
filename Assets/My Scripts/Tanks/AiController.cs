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

    /*
        >1.0 <2.0
        10  /  10.0 = 1.0
        11  /  10.0 = 1.1
        12  x       = 1.2
        13  x       = 1.3
        14  x       = 1.4
        15  x       = 1.5
        16  x       = 1.6
        17  x       = 1.7
        18  x       = 1.8
        19  x       = 1.9
        20  x       = 2.0
    */
}
