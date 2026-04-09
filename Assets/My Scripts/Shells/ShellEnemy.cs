using UnityEngine;

public class ShellEnemy : Shell
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLvl1") ||
            other.CompareTag("EnemyLvl2") ||
            other.CompareTag("EnemyLvl3") ||
            other.CompareTag("EnemyLvl4"))
            return;

        base.OnTriggerEnter2D(other);
    }
}
