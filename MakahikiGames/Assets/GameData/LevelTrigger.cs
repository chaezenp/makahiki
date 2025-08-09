using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string defaultTargetLevel = "Level1";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        string nextLevel = defaultTargetLevel;
        Debug.Log($"Loading: {nextLevel}");
        SceneManager.LoadScene(nextLevel);
    }
}
