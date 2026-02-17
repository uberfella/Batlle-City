using UnityEngine;

public class BrickDestroyOnHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShellPlayer"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.brickDestroyedSound);
        }
        //Debug.Log("OnTriggerEnter2D");
        if (other.gameObject.CompareTag("ShellPlayer") || other.gameObject.CompareTag("ShellEnemy"))
        {
            Destroy(gameObject);
            //Debug.Log("HIT");
        }
    }

}
