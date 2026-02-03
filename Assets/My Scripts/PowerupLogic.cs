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
    //temporary way to make sure the powerup spawns inside squares nicely
    float[] predefinedPosX = { -5.5f, -4.5f, -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f }; //13 entries
    float[] predefinedPosY = { -6.5f, -5.5f, -4.5f, -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f };

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnRandomPowerupOnField();
        } 
    }

    //-5.5 -6.5 
    //6.5 5.5
    public void SpawnRandomPowerupOnField()
    {
        float randomPosX = predefinedPosX[Random.Range(0, 12)];
        float randomPosY = predefinedPosY[Random.Range(0, 12)];
        int randomPowerUp = Random.Range(0, 6);
        //make sure the powerup doesn't spawn inside the base
        if((randomPosX == -0.5f & randomPosY == -6.5) || (randomPosX == 0.5f & randomPosY == -6.5f) || (randomPosX == 1.5f & randomPosY == -6.5f) || 
            (randomPosX == 0.5f & randomPosY == -5.5f) || (randomPosX == 0.5f & randomPosY == -5.5f) || (randomPosX == 1.5f & randomPosY == -5.5f))
        {
            randomPosX = predefinedPosX[Random.Range(0, 12)];
            randomPosY = predefinedPosY[Random.Range(2, 12)];
        }

        GameObject instance = Instantiate(powerupsToSpawn[randomPowerUp], new Vector2(randomPosX, randomPosY), Quaternion.identity);
        instance.SetActive(true);

        Destroy(instance, 1000f);

    }

}
