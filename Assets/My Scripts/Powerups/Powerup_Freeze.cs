using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Freeze : Powerup_Superclass
{
    public GameObject freezePowerupSprite;
    public SpriteRenderer spriteRenderer;

    private Spawner spawner;
    private bool wasActivated;

    void Start()
    {
        wasActivated = false;
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
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
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
        //yield return new WaitForSeconds(10f);
        yield return new WaitForSeconds(3f);
        foreach (Enemy e in frozenEnemies)
        {
            if (e != null)
                e.SetFrozen(false);
        }
        Destroy(gameObject);
    }
}
