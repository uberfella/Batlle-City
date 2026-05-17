using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup_Fortify : Powerup_Superclass
{
    public GameObject fortifyBlocksToSpawn; //fortify powerup concrete  
    public GameObject brickBlocksToSpawn; //fortify powerup concrete  
    public Vector2[] fortifySpawnPositions; //fortify concrete spawnpositions  
    public Vector2[] brickSpawnPositions; //fortify bricks spawnpositions  
    public List<bool> rotate90Z; //fortify powerup concrete rotations  
    public GameObject fortifyPowerupSprite;
    public SpriteRenderer spriteRenderer;
    public bool wasActivated;

    private List<GameObject> fortifySpawnedObjects = new List<GameObject>();
    private List<GameObject> brickSpawnedObjects = new List<GameObject>();
    private GameObject[] bricksInBase;

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
        if (wasActivated)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            wasActivated = true;
            base.OnTriggerEnter2D(other);
            StartCoroutine(FortifySpawnConcreteAndBricksOnBase());
            //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible while the base is fortified and then we destroy it
            fortifyPowerupSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator FortifySpawnConcreteAndBricksOnBase()
    {
        DespawnBricks();
        FortifySpawnConcrete();
        PowerupLogic.Instance.fortifyIsActive = true;
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
}
