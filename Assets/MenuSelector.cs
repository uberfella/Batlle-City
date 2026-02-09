using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public RectTransform selectorIcon;           // The arrow or icon object
    public List<RectTransform> buttonTargets;    // Button transforms to snap to
    public List<Button> buttonComponents;        // Matching Button components
    private int currentIndex = 0;
    private int offsetForDifferentScenes = -2;
    private KeyCode selectFirstKey = KeyCode.None;
    private KeyCode selectSecondKey = KeyCode.None;
    bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }

    void Start()
    {
        RefreshVisibleButtons();
        StartCoroutine(DeferredMoveSelector());
        //change variables depending on the current loaded scene
        if (IsScene("Main Menu"))
        {
            currentIndex = 0;
            offsetForDifferentScenes = 50;
            selectFirstKey = KeyCode.W;
            selectSecondKey = KeyCode.S;
        }
        else if (IsScene("Scoreboard"))
        {
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
    IEnumerator DeferredMoveSelector()
    {
        yield return null; // wait one frame
        MoveSelectorToCurrent(); // now layout is done
    }

    void Update()
    {
        if (Input.GetKeyDown(selectFirstKey))
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
            MoveSelectorToCurrent();
        }
        else if (Input.GetKeyDown(selectSecondKey))
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
        Vector3 newPos = new Vector3(targetPos.x - offsetForDifferentScenes, targetPos.y, targetPos.z);
        selectorIcon.position = newPos;
    }
}