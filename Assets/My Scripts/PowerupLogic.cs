using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum PowerupType { Fortify, Freeze, Invulnerability, KillAll, Levelup, Extralife }
public class DontDestroy : MonoBehaviour
{

}

public class PowerupLogic : MonoBehaviour
{
    public GameObject [] powerupsToSpawn;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //cheat to kill all
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameObject instance = Instantiate(powerupsToSpawn[3], new Vector2(-1.75f, -5.95f), Quaternion.identity);
            instance.SetActive(true);
        }
    }

    //-5.5 -6.5 
    //6.5 5.5
    public void SpawnRandomPowerupOnField()
    {
        int randomPowerUp = Random.Range(0, 6);
        //Debug.Log("randomPowerUp = " + randomPowerUp);
        float rollX = Random.Range(-5.5f, 6.5f);
        float rollY = Random.Range(-6.5f, 5.5f);
        //ensure the final pos is even to the grid
        float randomPosX = rollX - (rollX % 0.5f);
        Debug.Log("rollX = " + rollX);
        Debug.Log("rollX - (rollX % 0.5f) = " + randomPosX);
        float randomPosY = rollY - (rollY % 0.5f);
        GameObject instance = Instantiate(powerupsToSpawn[randomPowerUp], new Vector2(randomPosX, randomPosY), Quaternion.identity);
        instance.SetActive(true);

        Destroy(instance, 10f);

    }

}
