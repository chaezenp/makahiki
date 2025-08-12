using UnityEngine;

public class ResetLevelsWon : MonoBehaviour
{
    public LevelProgress levelProgress;

    public void ResetL()
    {
        levelProgress.ResetAllLevels();
    }
}
