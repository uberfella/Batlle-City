using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    public RectTransform selectorIcon;           // The arrow or icon object
    public List<RectTransform> buttonTargets;    // Button transforms to snap to
    public List<Button> buttonComponents;        // Matching Button components
    private int currentIndex = 0;

    void Start()
    {
        RefreshVisibleButtons();
        StartCoroutine(DeferredMoveSelector());
    }
    IEnumerator DeferredMoveSelector()
    {
        yield return null; // wait one frame
        MoveSelectorToCurrent(); // now layout is done
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
            MoveSelectorToCurrent();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = Mathf.Min(buttonTargets.Count - 1, currentIndex + 1);
            MoveSelectorToCurrent();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (currentIndex < buttonComponents.Count)
            {
                buttonComponents[currentIndex].onClick.Invoke();
            }
        }
    }

    void RefreshVisibleButtons()
    {
        // Remove hidden or inactive buttons
        for (int i = buttonTargets.Count - 1; i >= 0; i--)
        {
            if (!buttonTargets[i].gameObject.activeInHierarchy)
            {
                buttonTargets.RemoveAt(i);
                buttonComponents.RemoveAt(i);
            }
        }

        // Reset index in case current selection is now out of bounds
        currentIndex = Mathf.Clamp(currentIndex, 0, buttonTargets.Count - 1);
    }

    void MoveSelectorToCurrent()
    {

        if (buttonTargets.Count == 0) return;

        Vector3 targetPos = buttonTargets[currentIndex].position;
        Vector3 newPos = new Vector3(targetPos.x - 50, targetPos.y, targetPos.z);
        Debug.Log("targetPos.x = " + (targetPos.x - 50) + "targetPos.y" + (targetPos.y) + "targetPos.z" + (targetPos.z));
        selectorIcon.position = newPos;

        //selectorIcon.anchoredPosition = new Vector2(-200, buttonTargets[currentIndex].anchoredPosition.y);
        //Debug.Log("currentIndex = " + currentIndex);
        //Debug.Log("anchoredPosition.x = " + (buttonTargets[currentIndex].anchoredPosition.x) + "anchoredPosition.y" + (buttonTargets[currentIndex].anchoredPosition.y));

    }
}

//2 buttons - starts at random pos, continues stuck
//1 button -  
// 