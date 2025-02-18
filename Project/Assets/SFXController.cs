using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        audioSource.Play();
    }
}
