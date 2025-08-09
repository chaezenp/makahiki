using UnityEngine;

public class ResetGameProgress : MonoBehaviour
{
    public LevelProgress levelProgress;
    public SkipIntro skipIntro;

    public void ResetAllProgress()
    {
        // Reset PlayerPrefs entries
        PlayerPrefs.DeleteKey("IntroWatched");

        foreach (string levelName in levelProgress.levelSceneNames)
        {
            PlayerPrefs.DeleteKey($"Level_{levelName}_Won");
        }

        PlayerPrefs.Save();

        // Reset in-memory ScriptableObject data too
        levelProgress.ResetAllLevels();
        skipIntro.SetValue(false); // will also re-save as false

        Debug.Log("Game progress has been reset.");
    }
}
