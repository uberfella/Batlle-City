using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelectorScoreboard : MonoBehaviour
{
    public RectTransform selectorIcon;
    public List<Button> buttons;
    public DestroyedEnemiesDisplay destroyedEnemiesDisplay;

    private int currentIndex = 0;
    private int lastUsedIndex = 0;
    private KeyCode selectFirstKey = KeyCode.A;
    private KeyCode selectSecondKey = KeyCode.D;
    [SerializeField]private int offsetForDifferentScenes = 2;


    void Start()
    {

        if (!GameLogic.GameOver)
        {
            currentIndex = 1;
            lastUsedIndex = 0;
        }
        else
        {
            currentIndex = 0;
            lastUsedIndex = 1;
        }
        //MoveSelector is being executed in destroyedEnemiesDisplay script here
    }

    void Update()
    {
        if (Input.GetKeyDown(selectFirstKey) && destroyedEnemiesDisplay.scoreBoardHasFinishedDrawing)
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(selectSecondKey) && !GameLogic.GameOver && destroyedEnemiesDisplay.scoreBoardHasFinishedDrawing)
        {
            MoveRight();
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && destroyedEnemiesDisplay.scoreBoardHasFinishedDrawing)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

            buttons[currentIndex].onClick.Invoke();
        }
    }

    void MoveLeft()
    {
        currentIndex = 0;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 0;
        MoveSelector();
    }

    void MoveRight()
    {
        currentIndex = 1;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 1;

        MoveSelector();
    }

    public void MoveSelector()
    {
        if (buttons == null || buttons.Count == 0)
            return;

        RectTransform target = buttons[currentIndex].GetComponent<RectTransform>();

        RectTransform selectorRect = selectorIcon.GetComponent<RectTransform>();
        RectTransform targetRect = target.GetComponent<RectTransform>();

        selectorIcon.position = new Vector3(
            target.position.x - offsetForDifferentScenes,
            target.position.y,
            target.position.z
        );
    }
}
