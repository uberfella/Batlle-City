using UnityEngine;

public class ShellPlayer : Shell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        base.OnTriggerEnter2D(other);
    }

}
