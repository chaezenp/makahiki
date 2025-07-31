using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNExtLevel : MonoBehaviour
{
    public string nextLevel;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene(nextLevel);
        }

    }

}
