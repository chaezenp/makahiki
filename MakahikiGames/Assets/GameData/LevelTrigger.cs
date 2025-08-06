using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public LevelProgress levelProgress;

    [SerializeField] private string defaultTargetLevel = "Level1";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        string nextLevel = GetTargetLevel();
        Debug.Log($"Loading: {nextLevel}");
        SceneManager.LoadScene(nextLevel);
    }

    private string GetTargetLevel()
    {
        string current = defaultTargetLevel;

        // Check if current level is already completed
        if (levelProgress.HasWonLevel(current))
        {
            string next = levelProgress.GetNextLevelName(current);
            if (!string.IsNullOrEmpty(next))
                return next;
        }

        return current; // default if not completed
    }
}
