using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 5f;
    [SerializeField] private string nextSceneName;
    [SerializeField] SkipIntro skipIntro;

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(delayBeforeLoad));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MarkIntroAsWatched();
        SceneManager.LoadScene(nextSceneName);
    }
    public void MarkIntroAsWatched()
    {
        skipIntro.SetValue(true);
        Debug.Log("Intro marked as watched.");
    }
}