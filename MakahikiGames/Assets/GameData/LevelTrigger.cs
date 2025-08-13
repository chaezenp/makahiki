using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string defaultTargetLevel = "Level1";
    public FadeInOut fadeInOut;
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        fadeInOut.FadeIn();
        Invoke("PerformDelayedAction", fadeInOut.fadeDuration);
    }
    void PerformDelayedAction()
    {
        LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        string nextLevel = defaultTargetLevel;
        Debug.Log($"Loading: {nextLevel}");
        SceneManager.LoadScene(nextLevel);
    }
}
