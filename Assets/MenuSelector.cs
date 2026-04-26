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
    public GameObject menuGroup;
    public Canvas canvas;

    private int currentIndex = 0;
    private KeyCode selectFirstKey = KeyCode.W;
    private KeyCode selectSecondKey = KeyCode.S;
    private KeyCode selectFirstKeyConfirmPanel = KeyCode.A;
    private KeyCode selectSecondKeyConfirmPanel = KeyCode.D;
    [SerializeField] private int offsetForDifferentScenes = 70;
    private string lastScene;
    private int lastUsedIndex = 0;

    void Start()
    {
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

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {

                buttons[currentIndex].onClick.Invoke();
            }
        }
        else
        {
            if (Input.GetKeyDown(selectFirstKeyConfirmPanel) && PlayerPrefs.HasKey("HasSaveData"))
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(selectSecondKeyConfirmPanel))
            {
                MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                selectorIcon.SetParent(canvas.transform);

                buttonsConfirmPanel[currentIndex].onClick.Invoke();
            }
        }


    }

    public void MoveUp()
    {
        currentIndex = 1;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 1;
        MoveSelector(buttons);
    }

    public void MoveDown()
    {
        currentIndex = 0;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 0;
        MoveSelector(buttons);
    }

    public void MoveRight()
    {
        //no 0 right
        Debug.Log("no 0 right");
        //selectSecondKeyConfirmPanel = KeyCode.D
        currentIndex = 0;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 0;
        MoveSelector(buttonsConfirmPanel);
    }

    public void MoveLeft()
    {
        //yes 1 left
        Debug.Log("yes 1 left");
        //selectFirstKeyConfirmPanel = KeyCode.A;
        currentIndex = 1;
        if (currentIndex != lastUsedIndex)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);
        }
        lastUsedIndex = 1;
        MoveSelector(buttonsConfirmPanel);
    }

    void MoveSelector(List<Button> buttonsType)
    {
        if (buttonsType == null || buttonsType.Count == 0)
            return;

        RectTransform target = buttonsType[currentIndex].GetComponent<RectTransform>();

        if (!confirmStartPanel.activeInHierarchy)
        {
            selectorIcon.SetParent(canvas.transform);

            RectTransform selectorRect = selectorIcon.GetComponent<RectTransform>();
            RectTransform targetRect = target.GetComponent<RectTransform>();

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, targetRect.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                selectorRect.parent as RectTransform,
                screenPoint,
                null,
                out Vector2 localPoint
            );

            selectorRect.anchoredPosition = localPoint + new Vector2(-offsetForDifferentScenes, 0);
        }
        else
        {

            RectTransform selectorRect = selectorIcon.GetComponent<RectTransform>();
            RectTransform targetRect = target.GetComponent<RectTransform>();

            selectorIcon.SetParent(target.parent);
            selectorIcon.anchoredPosition = target.anchoredPosition;

            selectorRect.anchoredPosition = targetRect.anchoredPosition + new Vector2(-offsetForDifferentScenes, 0);

        }
    }

    IEnumerator ExecuteMoveSelectorAfterOneFrame()
    {
        yield return null;
        MoveSelector(buttons);
    }

    public void ResetSelection()
    {
        currentIndex = 0;
        lastUsedIndex = 0;
        MoveSelector(buttons); // no sound
    }
}