using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Fortify : Powerup_Superclass
{
    public GameObject objectToSpawn; //fortify powerup concrete  
    public Vector2[] spawnPositions; //fortify powerup spawnpositions  
    public List<bool> rotate90Z; //fortify powerup concrete rotations  
    public GameObject fortifyPowerupSprite;
    public SpriteRenderer spriteRenderer;
    public bool wasActivated;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        wasActivated = false;
    }
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !wasActivated)
        {
            wasActivated = true;
            base.OnTriggerEnter2D(other);
            StartCoroutine(FortifySpawnConcreteOnBase());
            //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible while the base is fortified and then we destroy it
            fortifyPowerupSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator FortifySpawnConcreteOnBase()
    {
        // Spawn for 5 seconds
        FortifySpawnConcrete();
        yield return new WaitForSeconds(10f);

        // Despawn for 1 second
        FortifyDespawnConcrete();
        yield return new WaitForSeconds(1f);

        // Spawn again for 5 seconds
        FortifySpawnConcrete();
        yield return new WaitForSeconds(1f);

        // Despawn for 1 second
        FortifyDespawnConcrete();
        yield return new WaitForSeconds(1f);

        // Spawn again for 5 seconds
        FortifySpawnConcrete();
        yield return new WaitForSeconds(1f);

        // Final despawn
        FortifyDespawnConcrete();
        Destroy(gameObject);
    }

    public void FortifySpawnConcrete()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Quaternion rotation = rotate90Z[i] ? Quaternion.Euler(0, 0, 90) : Quaternion.identity;
            GameObject instance = Instantiate(objectToSpawn, spawnPositions[i], rotation);
            instance.SetActive(true);
            spawnedObjects.Add(instance);
        }
    }

    public void FortifyDespawnConcrete()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
    }
}
