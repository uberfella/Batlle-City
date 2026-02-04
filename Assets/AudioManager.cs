using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("------------ Audio Source ------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------ Audio Clip ------------")]

    public AudioClip mainTheme;
    public AudioClip shotFired;
    public AudioClip gameOverJingle;
    public AudioClip playerTankStationarySound;
    public AudioClip playerTankMovingSound;

    bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }
    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Start()
    {
        if (!IsScene("Main Menu"))
        {
            musicSource.clip = mainTheme;
            //musicSource.Play();
        }
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

}
