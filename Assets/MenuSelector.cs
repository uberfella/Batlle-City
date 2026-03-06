using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    public RectTransform selectorIcon;
    public List<Button> buttons;
    public List<Button> buttonsConfirmPanel;
    public GameObject confirmStartPanel;

    private int currentIndex = 0;
    private KeyCode selectFirstKey = KeyCode.W;
    private KeyCode selectSecondKey = KeyCode.S;
    private int offsetForDifferentScenes = 50;
    private int lastUsedIndex = 0;
    private bool confirmPanelActive;

    void Start()
    {
        if (true)
        {

        }
        if (PlayerPrefs.HasKey("HasSaveData"))
        {
            currentIndex = 1;
            lastUsedIndex = 1;

        }
        else
        {
            currentIndex = 0;
            lastUsedIndex = 0;

        }
        StartCoroutine(ExecuteMoveSelectorAfterOneFrame());
    }

    void Update()
    {
        if (!confirmStartPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(selectFirstKey) && PlayerPrefs.HasKey("HasSaveData"))
            {
                MoveUp();
            }

            if (Input.GetKeyDown(selectSecondKey))
            {
                MoveDown();
            }
        }
        else 
        {

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    void MoveUp()
    {
        currentIndex = 1;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 1;

        MoveSelector(buttons);
    }

    void MoveDown()
    {
        currentIndex = 0;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 0;
        MoveSelector(buttons);
    }

    void MoveRight()
    {
        //no 0 right

    }

    void MoveLeft()
    {
        //yes 1 left

    }

    void MoveSelector(List<Button> buttonsType)
    {
        if (buttons == null || buttons.Count == 0)
            return;

        RectTransform target = buttonsType[currentIndex].GetComponent<RectTransform>();

        selectorIcon.position = new Vector3(
            target.position.x - offsetForDifferentScenes,
            target.position.y,
            target.position.z
        );
    }

    IEnumerator ExecuteMoveSelectorAfterOneFrame()
    {
        yield return null;
        MoveSelector(buttons);
    }
}