using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelectorEndScene : MonoBehaviour
{
    public RectTransform selectorIcon;
    public List<Button> buttons;

    private int currentIndex = 0;
    private int offsetForDifferentScenes = 5;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDecreasingLivesSound);

            buttons[currentIndex].onClick.Invoke();
        }
    }

    public void MoveSelector()
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
