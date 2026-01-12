using UnityEngine;
using UnityEngine.SceneManagement;

public static class PerformBootstrap 
{
    const string SceneName = "Main Menu";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        // traverse the currently loaded scenes
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
        {
            var candidate = SceneManager.GetSceneAt(sceneIndex);

            if (candidate.name == SceneName)
            {
                return;
            }
        }

        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
    }
}

public class BootstrappedData : MonoBehaviour
{
    public static BootstrappedData Instance { get; private set; } = null;
    public static int levelNum = 0;
    public static int finalLevelNum = 2;

    private void Awake()
    {
        // check if an instance already exists
        if (Instance != null)
        {
            Debug.LogError("Found another BootstrappedData on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // prevent the data from being unloaded
        DontDestroyOnLoad(gameObject);
    }

    public void Test()
    {
        Debug.Log("Bootstrap is working!");
    }
}
