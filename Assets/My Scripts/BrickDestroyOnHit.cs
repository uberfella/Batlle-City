using UnityEngine;

public class BrickDestroyOnHit : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("OnTriggerEnter2D");
        if (other.gameObject.CompareTag("ShellPlayer") || other.gameObject.CompareTag("ShellEnemy"))
        {
            Destroy(gameObject);
            //Debug.Log("HIT");
        }
    }

}
