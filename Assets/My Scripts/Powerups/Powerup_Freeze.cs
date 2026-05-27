using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Freeze : Powerup_Superclass, IDestroyablePowerup
{
    public GameObject freezePowerupSprite;
    public SpriteRenderer spriteRenderer;
    public bool wasActivated;

    private Spawner spawner;
    private int id;

    void Start()
    {
        wasActivated = false;
        id = gameObject.GetInstanceID();
        Debug.Log("Instantiating " + id + " with wasActivated = "+wasActivated);
        spawner = FindFirstObjectByType<Spawner>();
    }
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (wasActivated)
        {
            Debug.Log(id + " wasActivated is " + wasActivated + " returning");
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            wasActivated = true; //prevents multiple activations
            Debug.Log(id + " wasActivated = " + wasActivated + " returning");
            StartCoroutine(FreezeEnemies());
            //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible while the enemies are frozen and then we destroy it
            freezePowerupSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator FreezeEnemies()
    {
        Debug.Log(id + " Initiating FreezeEnemies()");
        List<Enemy> frozenEnemies = new List<Enemy>(Spawner.AliveEnemies);
        foreach (Enemy e in frozenEnemies)
        {
            if (e != null)
                e.SetFrozen(true);
        }
        Debug.Log(id + " setting alive enemies frozen, waiting 10f");
        yield return new WaitForSeconds(10f);
        foreach (Enemy e in frozenEnemies)
        {
            if (e != null)
                e.SetFrozen(false);
        }
        Debug.Log(id + " unfreezing enemies, destroying powerup");
        Destroy(gameObject);
    }

    public void DestroyPowerup(int damage)
    {
        if (wasActivated)
        {
            return;
        }
        Destroy(gameObject);

    }
}
