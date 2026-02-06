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
    public AudioClip shotFired;
    public AudioClip gameOverJingle;
    public AudioClip playerTankStationarySound;
    public AudioClip playerTankMovingSound;
    public AudioClip levelUpJingle;

    bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!IsScene("Main Menu"))
        {
            musicSource.clip = mainTheme;
            //musicSource.Play();
        }
        playerTankStationarySource.clip = playerTankStationarySound;
        playerTankMovingSource.clip = playerTankMovingSound;
        playerTankStationarySource.loop = true;
        playerTankMovingSource.loop = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void Play(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
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

}
