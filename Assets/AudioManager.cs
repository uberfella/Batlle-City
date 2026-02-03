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

    private void Start()
    {
        if (!IsScene("Main Menu")) 
        {
            musicSource.clip = mainTheme;
            musicSource.Play();
        }

    }
}
