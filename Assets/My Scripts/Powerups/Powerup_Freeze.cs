using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Freeze : MonoBehaviour
{
    public GameObject freezePowerupSprite;
    private Spawner spawner;
    private bool wasActivated;

    void Start()
    {
        wasActivated = false;
        spawner = FindFirstObjectByType<Spawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") & !wasActivated)
        {
            wasActivated = true; //prevents multiple activations
            StartCoroutine(FreezeEnemies());
            //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible while the enemies are frozen and then we destroy it
            freezePowerupSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator FreezeEnemies()
    {
        List<Enemy> frozenEnemies = new List<Enemy>(Spawner.AliveEnemies);
        foreach (Enemy e in frozenEnemies)
        {
            if (e != null)
                e.SetFrozen(true);
        }
        //GameLogic.Instance.isEnemiesFrozen = true;
        yield return new WaitForSeconds(10f);
        //GameLogic.Instance.isEnemiesFrozen = false;
        foreach (Enemy e in frozenEnemies)
        {
            if (e != null)
                e.SetFrozen(false);
        }
        Destroy(gameObject);
    }
}
