using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSpawner : MonoBehaviour
{
    public PlayerController2D playerController2D;
    public Transform spawnPoint;
    public GameObject playerPrefab;
    public static int playerLives = 2;
    public Text playerLivesUI;

    void Start()
    {
        playerController2D = FindFirstObjectByType<PlayerController2D>();
    }

    void Update()
    {
        if (!playerController2D.playerIsAlive && playerLives >= 0)
        {
            StartCoroutine(RespawnPlayer());
        }

        if (playerLives < 0)
        {
            Debug.Log("GAME OVER, out of lives");
        }

        playerLivesUI.text = playerLives.ToString();

    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before respawning (optional)

        if (!playerController2D.playerIsAlive) // Ensure player is still dead before respawning
        {
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            playerController2D = newPlayer.GetComponent<PlayerController2D>();
        }
    }
}
