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

        SaveProgress();
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
        SaveProgress();
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

    public void SaveProgress()
    {
        foreach (var level in levels)
        {
            PlayerPrefs.SetInt($"Level_{level.levelName}_Won", level.isWon ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        foreach (var levelName in levelSceneNames)
        {
            int isWon = PlayerPrefs.GetInt($"Level_{levelName}_Won", 0);
            LevelWinData level = levels.Find(l => l.levelName == levelName);
            if (level != null)
            {
                level.isWon = isWon == 1;
            }
            else
            {
                levels.Add(new LevelWinData { levelName = levelName, isWon = isWon == 1 });
            }
        }
    }
}

