using UnityEngine;

public class ChangeBGMusic : MonoBehaviour
{
    [Header("Music Settings")]
    public SoundType backgroundMusicType = SoundType.BACKGROUND;
    public float fadeOutTime = 1f;
    public float fadeInTime = 1f;
    public SoundType sfxBackgroundtype = SoundType.None;

    void Start()
    {
        SoundManager.PlayBackgroundMusicFade(backgroundMusicType, fadeOutTime, fadeInTime);
        if (sfxBackgroundtype != SoundType.None) SoundManager.PlaySFXLoop(sfxBackgroundtype, 0.1f);
        else SoundManager.StopSFXLoop();
    } 
}
