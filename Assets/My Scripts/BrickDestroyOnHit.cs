using UnityEngine;

public class BrickDestroyOnHit : MonoBehaviour, IDamageable, IExplodableTarget
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShellPlayer"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.brickDestroyedSound);
        }
        ////Debug.Log("OnTriggerEnter2D");
        //if (other.gameObject.CompareTag("ShellPlayer") || other.gameObject.CompareTag("ShellEnemy"))
        //{
        //    Destroy(gameObject);
        //    //Debug.Log("HIT");
        //}

    }

    public void TakeDamage(int damage)
    {
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.brickDestroyedSound);
        Destroy(gameObject);
    }

}
