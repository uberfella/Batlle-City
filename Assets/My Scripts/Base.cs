using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject surrenderFlagSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShellPlayer")|| other.gameObject.CompareTag("ShellEnemy"))
        {
            surrenderFlagSprite.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject);
            GameLogic.GameOver = true;
        }
    }
}
