using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject playerPrefab;
    public static int playerLives = 2;
    public Text playerLivesUI;

    private PlayerController2D playerController2D;
    private GameOverSequence gameOverSequence;

    void Start()
    {
        gameOverSequence = FindFirstObjectByType<GameOverSequence>();
        playerController2D = FindFirstObjectByType<PlayerController2D>();

        UpdatePlayerLivesUI();

    }

    void Update()
    {
        //if (!playerController2D.playerIsAlive && playerLives > 0)
        //{
        //    StartCoroutine(RespawnPlayer());
        //}

        //if (playerLives <= 0)
        //{
        //    gameOverSequence.TriggerGameOver();
        //}

        //if (playerLives >= 0)
        //{
        //    playerLivesUI.text = playerLives.ToString();
        //}
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

    private void OnEnable()
    {
        PlayerController2D.OnDestroyed += OnObjectDestroyed;
    }

    private void OnDisable()
    {
        PlayerController2D.OnDestroyed -= OnObjectDestroyed;
    }

    private void OnObjectDestroyed(PlayerController2D obj)
    {
        UpdatePlayerLivesUI();
        if (!playerController2D.playerIsAlive && playerLives > 0)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private void UpdatePlayerLivesUI()
    {
        playerLivesUI.text = playerLives.ToString();
    }
}
