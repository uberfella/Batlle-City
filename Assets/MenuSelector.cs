using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    public RectTransform selectorIcon;
    public List<Button> buttons;

    private int currentIndex = 0;
    private KeyCode selectFirstKey = KeyCode.None;
    private KeyCode selectSecondKey = KeyCode.None;
    private int offsetForDifferentScenes = -2;

    void Start()
    {
        MoveSelector();
        if (IsScene("Main Menu"))
        {
            currentIndex = 0;
            offsetForDifferentScenes = 50;
            selectFirstKey = KeyCode.W;
            selectSecondKey = KeyCode.S;
        }
        else if (IsScene("Scoreboard"))
        {
            //selectorIcon.gameObject.SetActive(true);

            if (!GameLogic.GameOver)
            {
                currentIndex = 1;
            }
            else
            {
                currentIndex = 0;
            }
            offsetForDifferentScenes = 2;
            selectFirstKey = KeyCode.A;
            selectSecondKey = KeyCode.D;
        }
    }

    private bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }

    void Update()
    {
        if (Input.GetKeyDown(selectFirstKey))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(selectSecondKey))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    void MoveUp()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = buttons.Count - 1;

        MoveSelector();
    }

    void MoveDown()
    {
        currentIndex++;

        if (currentIndex >= buttons.Count)
            currentIndex = 0;

        MoveSelector();
    }

    void MoveSelector()
    {
        if (buttons == null || buttons.Count == 0)
            return;

        RectTransform target = buttons[currentIndex].GetComponent<RectTransform>();

        selectorIcon.position = new Vector3(
            target.position.x - offsetForDifferentScenes,
            target.position.y,
            target.position.z
        );
    }
}