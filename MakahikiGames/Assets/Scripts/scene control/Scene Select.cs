using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    public float Fadeduration = 1f;
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.LogWarning("You forgot to add a scene name lol!");
        }
    }

    public void fadeIn(string sceneName)
    {
        StartCoroutine(MyTimer(sceneName));
    }

    private IEnumerator MyTimer(string sceneName)
    {
        Debug.Log("Timer started at: " + Time.time);
        yield return new WaitForSeconds(Fadeduration);
        LoadSceneByName(sceneName);
        Debug.Log("Timer finished at: " + Time.time);
    }
}