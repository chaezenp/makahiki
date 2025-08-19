using UnityEngine;
using UnityEngine.UI;

public class OPtionsMenuSounds : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // Load saved values or default
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        ApplyVolumeSettings();
    }

    public void OnMasterVolumeChanged(float value)
    {
        SoundManager.SetMasterVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private void ApplyVolumeSettings()
    {
        Debug.Log("Sliders Update");
        OnMasterVolumeChanged(masterSlider.value);
        OnMusicVolumeChanged(musicSlider.value);
        OnSFXVolumeChanged(sfxSlider.value);
    }
    private void OnApplicationQuit()
{
    PlayerPrefs.Save();
}

}
