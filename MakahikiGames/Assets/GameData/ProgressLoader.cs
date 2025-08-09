using UnityEngine;

public class ProgressLoader : MonoBehaviour
{
    public LevelProgress levelProgress;
    public SkipIntro skipIntro;

    private void Awake()
    {
        levelProgress.LoadProgress();
        skipIntro.LoadValue();
    }
}

