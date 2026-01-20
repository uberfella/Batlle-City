using UnityEngine;
using UnityEngine.UI;

public class LevelNumDisplay : MonoBehaviour
{
    public Text levelNumText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelNumText.text = (GameLogic.levelNum + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
