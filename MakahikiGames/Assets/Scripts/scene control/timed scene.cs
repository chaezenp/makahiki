using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 5f;
    [SerializeField] private string nextSceneName;

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(delayBeforeLoad));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}