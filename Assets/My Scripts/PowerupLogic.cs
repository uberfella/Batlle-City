using System.Collections;
using UnityEngine;

public enum PowerupType { Fortify, Freeze, Invulnerability, KillAll, Levelup, Extralife }
public class PowerupLogic : MonoBehaviour
{
    public PowerupType powerupType;
    public GameObject freezePowerupSprite;

    private PlayerController2D playerController2D;
    private Enemy enemy;

    private void Awake()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(FreezeEnemies());
        }
    }

    //Player picks up the powerup
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //powerup type
            switch (powerupType)
            {
                case PowerupType.Levelup:
                    playerController2D.PlayerLevelUp();
                    Destroy(gameObject);
                    break;
                case PowerupType.Freeze:
                    StartCoroutine(FreezeEnemies());
                    //we can't destroy powerup gameobject here, otherwise the coroutine won't operate properly. So we just make it invisible
                    freezePowerupSprite.GetComponent<SpriteRenderer>().enabled = false;
                    break;
                case PowerupType.Invulnerability:
                    playerController2D.TriggerInvincibility();
                    Destroy(gameObject);
                    break;                
                case PowerupType.KillAll:
                    TryToKillAll();
                    Destroy(gameObject);
                    break;
            }
        }
    }

    IEnumerator FreezeEnemies()
    {
        GameLogic.Instance.isEnemiesFrozen = true;
        yield return new WaitForSeconds(5f);
        GameLogic.Instance.isEnemiesFrozen = false;
        Destroy(gameObject);
    }

    private void TryToKillAll()
    {
        //enemy.TakeDamage();
    }

}
