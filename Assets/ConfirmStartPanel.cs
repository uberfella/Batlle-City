using UnityEngine;
using UnityEngine.SceneManagement;


public class ConfirmStartPanel : MonoBehaviour
{
    public GameObject confirmPanel;   // Drag panel here in Inspector
    private MenuSelector menuSelector;

    private void OnEnable()
    {
        menuSelector = FindFirstObjectByType<MenuSelector>();
        menuSelector.MoveRight();
    }

    private void OnDisable()
    {
        menuSelector = FindFirstObjectByType<MenuSelector>();
        menuSelector.MoveDown();
    }
}
