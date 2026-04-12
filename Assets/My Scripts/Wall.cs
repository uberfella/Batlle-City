using UnityEngine;

public class Wall : MonoBehaviour, IExplodableTarget
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShellPlayer"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.obstacleHitButNotDestroyedSound);
        }
    }
}
