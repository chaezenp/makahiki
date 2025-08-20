using UnityEngine;
using UnityEngine.UI;

public class OPtionsMenuSounds : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

private void Start()
{
    masterSlider.value = SoundManager.GetMasterVolume();
    musicSlider.value = SoundManager.GetMusicVolume();
    sfxSlider.value = SoundManager.GetSFXVolume();

    ApplyVolumeSettings(); // Optional, only needed if you want to apply again
}

    public void OnMasterVolumeChanged(float value)
    {
        SoundManager.SetMasterVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        Debug.Log("Master Slider Vol: " + value);

    }

    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        Debug.Log("Music Slider Vol: " + value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        Debug.Log("SFX Slider Vol: " + value);
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
