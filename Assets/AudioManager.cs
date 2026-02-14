using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("------------ Audio Source ------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource playerTankStationarySource;
    [SerializeField] AudioSource playerTankMovingSource;

    [Header("------------ Audio Clip ------------")]
    public AudioClip mainTheme;
    public AudioClip endTheme;
    public AudioClip shotFired;
    public AudioClip gameOverJingle;
    public AudioClip playerTankStationarySound;
    public AudioClip playerTankMovingSound;
    public AudioClip levelUpJingle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsLevelScene(scene))
        {
            //PlayMainTheme();
        }

        //if (IsEndScene(scene))
        //{
        //    PlayEndingTheme();
        //}

    }

    private bool IsLevelScene(Scene scene)
    {
        return scene.name.StartsWith("Level");
        // or use build index, or a list, your choice
    }

    private bool IsEndScene(Scene scene)
    {
        return scene.name.StartsWith("End");
        // or use build index, or a list, your choice
    }

    private void PlayMainTheme()
    {
        if (musicSource.clip == mainTheme && musicSource.isPlaying)
            return;

        musicSource.clip = mainTheme;
        musicSource.Play();
    }

    private void PlayEndingTheme()
    {
        if (musicSource.clip == endTheme && musicSource.isPlaying)
            return;

        musicSource.clip = endTheme;
        musicSource.Play();
    }

    private void Start()
    {
        playerTankStationarySource.clip = playerTankStationarySound;
        playerTankMovingSource.clip = playerTankMovingSound;
        playerTankStationarySource.loop = true;
        playerTankMovingSource.loop = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void SetEngineState(bool moving)
    { 

        if (moving)
        {
            if (!playerTankMovingSource.isPlaying) playerTankMovingSource.Play();
            playerTankStationarySource.Pause();
        }
        else
        {
            if (!playerTankStationarySource.isPlaying) playerTankStationarySource.Play();
            playerTankMovingSource.Pause();
        }
    }
    public void StopEngineSound()
    {
        playerTankStationarySource.Stop();
        playerTankMovingSource.Stop();
    }
}
