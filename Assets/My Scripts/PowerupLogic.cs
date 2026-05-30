using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public enum PowerupType { Fortify, Freeze, Invulnerability, KillAll, Levelup, Extralife }

public class PowerupLogic : MonoBehaviour
{
    public GameObject [] powerupsToSpawn;
    public static PowerupLogic Instance;
    private bool fortifyPowerupCoroutineWasActivated;   
    //temporary way to make sure the powerup spawns inside squares nicely
    //-5.5 -5.5 
    //6.5 6.5
    float[] predefinedPosX = { -5.5f, -4.5f, -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f }; //13 entries
    float[] predefinedPosY = { -5.5f, -4.5f, -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f };

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        ////cheat to kill all
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameObject instance = Instantiate(powerupsToSpawn[3], new Vector2(-1.5f, -4.45f), Quaternion.identity);
            instance.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnRandomPowerupOnField();
        }

    }


    public void SpawnRandomPowerupOnField()
    {
        DestroyAllPowerUps();

        float randomPosX = predefinedPosX[Random.Range(0, 13)];
        float randomPosY = predefinedPosY[Random.Range(0, 13)];
        int randomPowerUp = Random.Range(0, 6);
        //make sure the powerup doesn't spawn inside the base
        if (((randomPosY == -5.5f || randomPosY == -4.5f) && (randomPosX == -0.5f || randomPosX == 0.5f || randomPosX == 1.5f)))
        {
            randomPosX = predefinedPosX[Random.Range(0, 13)];
            randomPosY = predefinedPosY[Random.Range(2, 13)];
        }

        GameObject instance = Instantiate(powerupsToSpawn[randomPowerUp], new Vector2(randomPosX, randomPosY), Quaternion.identity);
        //instance = Instantiate(powerupsToSpawn[randomPowerUp], new Vector2(randomPosX, randomPosY), Quaternion.identity);
        //instance.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.powerUpSpawn);

        //Destroy(instance, 10f);
    }

    public void DestroyAllPowerUps()
    {
        //GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

        //foreach (GameObject obj in powerups)
        //{
        //    if (obj == null) continue;
        //    if (obj != null)
        //    {
        //        //TODO remove reference to wasActivated
        //        //make OOP call destroy on everything and then powerups decide what to do when destroy is called
        //        if (obj.name == "Powerup_Fortify" && powerup_Fortify.wasActivated)
        //        {
        //            continue;
        //        }
        //        if (obj.name == "Powerup_Freeze" && powerup_Freeze.wasActivated)
        //        {
        //            continue;
        //        }
        //        Debug.Log("DestroyAllPowerUps(): destroying " + obj);
        //        UnityEngine.Object.Destroy(obj);
        //    }
        //}
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

        foreach (GameObject obj in powerups)
        {
            IDestroyablePowerup target = obj.GetComponent<IDestroyablePowerup>();

            if (target != null)
            {
                target.DestroyPowerup(1);
            }
        }

    }

    public bool GetFortifyPowerupCoroutineWasActivated()
    {
        return fortifyPowerupCoroutineWasActivated;
    }
    public void SetFortifyPowerupCoroutineWasActivated(bool var)
    {
        fortifyPowerupCoroutineWasActivated = var;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fortifyPowerupCoroutineWasActivated = false;
    }
}
