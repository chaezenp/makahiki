using UnityEngine;

public class FirstLaunchReset : MonoBehaviour
{
    private const string FirstLaunchKey = "HasLaunchedBefore";
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(FirstLaunchKey))
        {
            Debug.Log("First launch — resetting intro and level progress.");

            // Reset intro
            PlayerPrefs.DeleteKey("IntroWatched");

            // Reset levels (example: levelSceneNames = ["Level1", "Level2"])
            string[] levelNames = { "Level1", "Level2", "Level3" }; // update with your actual levels
            foreach (string levelName in levelNames)
            {
                PlayerPrefs.DeleteKey($"Level_{levelName}_Won");
            }

            PlayerPrefs.SetInt(FirstLaunchKey, 1);
            PlayerPrefs.Save();
        }
    }
}
