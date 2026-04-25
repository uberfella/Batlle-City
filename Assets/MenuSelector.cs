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
    public Canvas canvas;

    private int currentIndex = 0;
    private KeyCode selectFirstKey = KeyCode.W;
    private KeyCode selectSecondKey = KeyCode.S;
    private KeyCode selectFirstKeyConfirmPanel = KeyCode.A;
    //private KeyCode selectFirstKeyConfirmPanel = KeyCode.D;
    private KeyCode selectSecondKeyConfirmPanel = KeyCode.D;
    //private KeyCode selectSecondKeyConfirmPanel = KeyCode.A;
    [SerializeField] private int offsetForDifferentScenes = 70;
    private int lastUsedIndex = 0;
    private bool confirmPanelActive;

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
                AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

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
                AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

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
        Debug.Log("target = " + target);
        Debug.Log("target.anchoredPosition = " + target.anchoredPosition);

        if (!confirmStartPanel.activeInHierarchy)
        {
            selectorIcon.SetParent(canvas.transform);
            //selectorIcon.localScale = Vector3.one;
            //selectorIcon.anchoredPosition = target.anchoredPosition;

            //selectorIcon.position = new Vector3(
            //    target.position.x - offsetForDifferentScenes,
            //    target.position.y,
            //    target.position.z
            //);
            //selectorIcon.position = target.position;

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
            //selectorIcon.localScale = Vector3.one;
            selectorIcon.anchoredPosition = target.anchoredPosition;

            selectorRect.anchoredPosition = targetRect.anchoredPosition + new Vector2(-offsetForDifferentScenes, 0);

            ////selectorIcon.anchoredPosition = target.anchoredPosition;
            //Debug.Log("selectorIcon.anchoredPosition = " + selectorIcon.anchoredPosition);
            //Vector2 localPoint;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //    selectorIcon.parent as RectTransform,
            //    target.position,
            //    null,
            //    out localPoint
            //);

            //selectorIcon.anchoredPosition = localPoint;
            //selectorIcon.anchoredPosition = new Vector2(
            //    localPoint.x - 55,
            //    localPoint.y
            //);
            //selectorIcon.position = target.position;
        }



    }

    IEnumerator ExecuteMoveSelectorAfterOneFrame()
    {
        yield return null;
        MoveSelector(buttons);
    }
}