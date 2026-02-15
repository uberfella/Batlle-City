using UnityEngine;

public class Powerup_KillAll : Powerup_Superclass
{
    private Enemy enemy;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            //TryToKillAll();
            Enemy.DestroyAllInLayer();
            Destroy(gameObject);
        }
    }

    //why?
    //public void TryToKillAll()
    //{
    //    Enemy.DestroyAllInLayer();
    //}
}
