using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
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
}