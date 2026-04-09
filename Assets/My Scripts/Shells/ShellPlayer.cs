using UnityEngine;

public class ShellPlayer : Shell
{
    
    void Start()
    {
        
    }

    void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        base.OnTriggerEnter2D(other);
    }

}
