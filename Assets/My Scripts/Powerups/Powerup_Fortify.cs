using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup_Fortify : Powerup_Superclass, IDestroyablePowerup
{
    public GameObject fortifyBlocksToSpawn; //fortify powerup concrete  
    public GameObject brickBlocksToSpawn; //fortify powerup concrete  
    public Vector2[] fortifySpawnPositions; //fortify concrete spawnpositions  
    public Vector2[] brickSpawnPositions; //fortify bricks spawnpositions  
    public List<bool> rotate90Z; //fortify powerup concrete rotations  
    public GameObject fortifyPowerupSprite;
    public SpriteRenderer spriteRenderer;

    private List<GameObject> fortifySpawnedObjects = new List<GameObject>();
    private List<GameObject> brickSpawnedObjects = new List<GameObject>();
    private GameObject[] bricksInBase;
    private bool wasActivated;

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
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (wasActivated)
        {
            return;
        }

        if (PowerupLogic.Instance.GetFortifyPowerupCoroutineWasActivated())
        {
            Debug.Log("GetFortifyPowerupCoroutineWasActivated() = "+ PowerupLogic.Instance.GetFortifyPowerupCoroutineWasActivated());
            base.OnTriggerEnter2D(other);
            Destroy(gameObject);
            return;
        }

        base.OnTriggerEnter2D(other);
        wasActivated = true;
        StartCoroutine(FortifySpawnConcreteAndBricksOnBase());
        //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible while the base is fortified and then we destroy it
        fortifyPowerupSprite.GetComponent<SpriteRenderer>().enabled = false;

    }

    IEnumerator FortifySpawnConcreteAndBricksOnBase()
    {
        PowerupLogic.Instance.SetFortifyPowerupCoroutineWasActivated(true);
        Debug.Log("PowerupLogic.Instance.fortifyPowerupCoroutineWasActivated = " + PowerupLogic.Instance.GetFortifyPowerupCoroutineWasActivated());
        DespawnBricks();
        FortifySpawnConcrete();
        yield return new WaitForSeconds(10f);

        FortifyDespawnConcrete();
        SpawnBricks();
        yield return new WaitForSeconds(1f);

        DespawnBricks();
        FortifySpawnConcrete();
        yield return new WaitForSeconds(1f);

        FortifyDespawnConcrete();
        SpawnBricks();
        yield return new WaitForSeconds(1f);

        DespawnBricks();
        FortifySpawnConcrete();
        yield return new WaitForSeconds(1f);

        FortifyDespawnConcrete();
        SpawnBricks();
        PowerupLogic.Instance.SetFortifyPowerupCoroutineWasActivated(false);
        Destroy(gameObject);
    }

    public void FortifySpawnConcrete()
    {
        for (int i = 0; i < fortifySpawnPositions.Length; i++)
        {
            Quaternion rotation = rotate90Z[i] ? Quaternion.Euler(0, 0, 90) : Quaternion.identity;
            GameObject instance = Instantiate(fortifyBlocksToSpawn, fortifySpawnPositions[i], rotation);
            instance.SetActive(true);
            fortifySpawnedObjects.Add(instance);
        }
    }
    public void SpawnBricks()
    {
        bricksInBase = GameObject.FindGameObjectsWithTag("Brick_Base");
        for (int i = 0; i < brickSpawnPositions.Length; i++)
        {
            GameObject instance = Instantiate(brickBlocksToSpawn, brickSpawnPositions[i], Quaternion.identity);
            instance.SetActive(true);
            brickSpawnedObjects.Add(instance);
        }
    }

    public void FortifyDespawnConcrete()
    {
        foreach (GameObject obj in fortifySpawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        fortifySpawnedObjects.Clear();
    }

    public void DespawnBricks()
    {
        bricksInBase = GameObject.FindGameObjectsWithTag("Brick_Base");
        foreach (GameObject obj in bricksInBase)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        foreach (GameObject obj in brickSpawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        brickSpawnedObjects.Clear();
    }

    public void DestroyPowerup(int damage)
    {
        Debug.Log("TakeDamage");
        if (wasActivated)
        {
            return;
        }
        Destroy(gameObject);
    }
}
