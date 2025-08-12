using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntroButton : MonoBehaviour
{
    public string nextscene;
    public SkipIntro skipIntro;
    public GameObject SkipButton;
    public bool canSkip = false;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup canvasGroup2;
    [SerializeField] private float fadeDuration = 1f;

    void Start()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        if (canvasGroup2 != null)
        {
            canvasGroup2.alpha = 0f;
        }
        if (skipIntro.HasWatchedIntro())
        {
            canSkip = true;
        }
        if (canSkip)
        {
            FadeIn();
        }

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
    }
    public void SkipIntro()
    {
        FadeIn2();
    }
    public void FadeIn2()
    {
        StartCoroutine(FadeIn2Routine());
    }

    private IEnumerator FadeIn2Routine()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            canvasGroup2.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup2.alpha = 1f;
        SceneManager.LoadScene(nextscene);

    }
}
