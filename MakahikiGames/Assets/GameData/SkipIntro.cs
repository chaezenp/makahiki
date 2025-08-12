using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkipIntro", menuName = "Game/Skip Intro")]
public class SkipIntro : ScriptableObject
{
    public SkipIntroData scene;

    private const string PlayerPrefsKey = "IntroWatched";

    public void SetValue(bool newValue)
    {
        scene.watchedOnce = newValue;
        PlayerPrefs.SetInt(PlayerPrefsKey, newValue ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool HasWatchedIntro()
    {
        return scene.watchedOnce;
    }

    public void LoadValue()
    {
        scene.watchedOnce = PlayerPrefs.GetInt(PlayerPrefsKey, 0) == 1;
    }
}

