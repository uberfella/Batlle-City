using System.Collections;
using UnityEngine;

public class Powerup_Freeze : MonoBehaviour
{
    public GameObject freezePowerupSprite;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FreezeEnemies());
            //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible while the enemies are frozen and then we destroy it
            freezePowerupSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator FreezeEnemies()
    {
        GameLogic.Instance.isEnemiesFrozen = true;
        yield return new WaitForSeconds(5f);
        GameLogic.Instance.isEnemiesFrozen = false;
        Destroy(gameObject);
    }
}
