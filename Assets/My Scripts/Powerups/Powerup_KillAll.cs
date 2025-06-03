using UnityEngine;

public class Powerup_KillAll : MonoBehaviour
{
    private Enemy enemy;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        TryToKillAll();
        Destroy(gameObject);
    }

    private void TryToKillAll()
    {
        Enemy.DestroyAllInLayer();
    }
}
