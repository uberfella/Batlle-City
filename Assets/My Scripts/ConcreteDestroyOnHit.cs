using UnityEngine;

public class ConcreteDestroyOnHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerProperties.playerLevel >= 3 && other.gameObject.CompareTag("ShellPlayer"))
        {
            Destroy(gameObject);
        }
    }
}   
