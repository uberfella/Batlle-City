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

    bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //if (!IsScene("Main Menu")/* || !IsScene("Bootstrapped Scene")*/)
        //{
        //    musicSource.clip = mainTheme;
        //    musicSource.Play();
        //}
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMainTheme()
    {
        if (!IsScene("Main Menu")/* || !IsScene("Bootstrapped Scene")*/)
        {
            musicSource.clip = mainTheme;
            musicSource.Play();
        }
    }
}
