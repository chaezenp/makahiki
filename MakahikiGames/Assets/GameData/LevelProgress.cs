using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgress", menuName = "Game/Level Progress")]
public class LevelProgress : ScriptableObject
{
    [Header("Scene Names in Order")]
    public List<string> levelSceneNames = new List<string>();
    public List<LevelWinData> levels = new List<LevelWinData>();

    public void SetLevelWon(string levelName)
    {
        LevelWinData level = levels.Find(l => l.levelName == levelName);
        if (level != null)
        {
            level.isWon = true;
        }
        else
        {
            levels.Add(new LevelWinData { levelName = levelName, isWon = true });
        }
    }

    public bool HasWonLevel(string levelName)
    {
        LevelWinData level = levels.Find(l => l.levelName == levelName);
        return level != null && level.isWon;
    }

    public void ResetAllLevels()
    {
        foreach (var level in levels)
        {
            level.isWon = false;
        }
    }
    public string GetNextLevelName(string currentLevel)
{
    int index = levelSceneNames.IndexOf(currentLevel);
    if (index >= 0 && index < levelSceneNames.Count - 1)
    {
        return levelSceneNames[index + 1];
    }
    return null;
}
}
