using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum PowerupType { Fortify, Freeze, Invulnerability, KillAll, Levelup, Extralife }
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

public class PowerupLogic : MonoBehaviour
{
    public GameObject [] powerupsToSpawn;

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
        float randomPosX = Random.Range(-5.5f, 6.5f);
        float randomPosY = Random.Range(-6.5f, 5.5f);
        GameObject instance = Instantiate(powerupsToSpawn[randomPowerUp], new Vector2(randomPosX, randomPosY), Quaternion.identity);
        instance.SetActive(true);

        Destroy(instance, 10f);

    }

}
