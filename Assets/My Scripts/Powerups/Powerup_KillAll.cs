using UnityEngine;

public class Powerup_KillAll : MonoBehaviour
{
    private Enemy enemy;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TryToKillAll();
            Destroy(gameObject);
        }
    }

    public void TryToKillAll()
    {
        Enemy.DestroyAllInLayer();
    }
}
