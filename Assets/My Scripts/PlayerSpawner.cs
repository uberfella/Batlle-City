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
        if (playerLives > 0)
        {
            if(!playerController2D.playerIsAlive)
            StartCoroutine(RespawnPlayer());
        }
        else
        {
           gameOverSequence.TriggerGameOver();
        }
    }

    private void UpdatePlayerLivesUI()
    {
        playerLivesUI.text = playerLives.ToString();
    }
}
