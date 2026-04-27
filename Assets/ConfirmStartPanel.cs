using UnityEngine;
using UnityEngine.SceneManagement;


public class ConfirmStartPanel : MonoBehaviour
{
    public GameObject confirmPanel;
    public MenuSelector menuSelector;

    private void OnEnable()
    {
        if (menuSelector != null)
            menuSelector.MoveRight();

    }

    private void OnDisable()
    {
        if (menuSelector != null)
            menuSelector.MoveDown();
    }
}
