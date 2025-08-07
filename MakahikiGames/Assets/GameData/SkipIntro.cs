using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkipIntro", menuName = "Game/Skip Intro")]
public class SkipIntro : ScriptableObject
{
    public SkipIntroData scene;
        public void SetValue(bool newValue)
        {
            scene.watchedOnce = newValue;
        }

    public bool HasWatchedIntro()
    {
        return scene.watchedOnce;
    }
}
