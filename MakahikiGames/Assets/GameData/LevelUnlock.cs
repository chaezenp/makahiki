using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    public string targetLevelName;                // The scene this portal leads to
    public LevelProgress levelProgress;           // Reference to the LevelProgress asset
    public GameObject levelTrigger;
    public GameObject level2Indicator;
    public GameObject blockBush;

    void Start()
    {
        // Find the previous level
        string previousLevel = GetPreviousLevelName();

        // Only activate this portal if the previous level was won (or it's the first level)
        if (string.IsNullOrEmpty(previousLevel) || levelProgress.HasWonLevel(previousLevel))
        {
            levelTrigger.SetActive(true); // Show portal
            level2Indicator.SetActive(true);
            blockBush.SetActive(false);
        }
        else
        {
            levelTrigger.SetActive(false); // Hide or lock portal
            level2Indicator.SetActive(false);
            blockBush.SetActive(true);
        }
    }

    private string GetPreviousLevelName()
    {
        int index = levelProgress.levelSceneNames.IndexOf(targetLevelName);
        if (index > 0)
        {
            return levelProgress.levelSceneNames[index - 1];
        }
        return null;
    }
}
