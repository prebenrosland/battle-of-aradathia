using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start() 
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume (float volume)
    {
        audioMixer.SetFloat("MasterParameter", volume);
    }

    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("MusicParameter", volume);
    }

    public void SetSFXVolume (float volume)
    {
        audioMixer.SetFloat("SFXParameter", volume);
    }

    public void SetGraphicsQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

}
