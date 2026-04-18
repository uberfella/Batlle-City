using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject playerPrefab;

    public Text playerLivesUI;

    private PlayerController2D playerController2D;
    private GameOverSequence gameOverSequence;

    void Start()
    {
        gameOverSequence = FindFirstObjectByType<GameOverSequence>();
        playerController2D = FindFirstObjectByType<PlayerController2D>();

        UpdatePlayerLivesUI();

    }

    private IEnumerator RespawnPlayer()
    {

        yield return new WaitForSeconds(1f);

        if (GameLogic.GameOver)
        {
            yield break;
        }

        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        playerController2D = newPlayer.GetComponent<PlayerController2D>();
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
        if (PlayerProperties.playerLives < 0)
        {
            gameOverSequence.TriggerGameOver();
            return;
        }
        UpdatePlayerLivesUI();
        StartCoroutine(RespawnPlayer());
    }

    public void UpdatePlayerLivesUI()
    {
        if (PlayerProperties.playerLives < 0)
        {
            return;
        }
        playerLivesUI.text = PlayerProperties.playerLives.ToString();
    }
}
