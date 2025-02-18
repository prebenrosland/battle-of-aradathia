using UnityEngine;

public class MainMenuMusicController : MonoBehaviour
{
    private AudioSource mainMenuMusic;

    void Start()
    {
        mainMenuMusic = GetComponentInChildren<AudioSource>();
        mainMenuMusic.Play();

        DontDestroyOnLoad(gameObject);
    }
}
