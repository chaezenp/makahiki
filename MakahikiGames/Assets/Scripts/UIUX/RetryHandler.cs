using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RetryHandler : MonoBehaviour
{
    public string talkArea = "TalkArea";

    public Button firstButton;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    private bool isQuit = false;
    private bool isRetry = false;

    void OnEnable()
    {
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }

    public void Retry()
    {
        isRetry = true;
        FadeIn();
    }

    public void Quit()
    {
        isQuit = true;
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        if (isQuit)
        {
            SceneManager.LoadScene(talkArea);
            isQuit = false;
        }

        if (isRetry)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            isRetry = false;
        }
    }
}
