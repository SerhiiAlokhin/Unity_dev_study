using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup loadingPanel;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject hud;

    [Header("Timings")]
    [SerializeField] private float fakeLoadSeconds = 2f;
    [SerializeField] private float fadeTime = 0.25f;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        ForceShow(loadingPanel);
        ForceHide(mainMenuPanel);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (hud) hud.SetActive(false);

        StartCoroutine(LoadingFlow());
    }

    IEnumerator LoadingFlow()
    {
        if (loadingBar) loadingBar.value = 0f;

        float t = 0f;
        while (t < fakeLoadSeconds)
        {
            t += Time.deltaTime;
            if (loadingBar) loadingBar.value = Mathf.Clamp01(t / fakeLoadSeconds);
            yield return null;
        }

        yield return Fade(loadingPanel, 0f);
        yield return Fade(mainMenuPanel, 1f);

        if (gameManager) gameManager.StopGame();
    }

 
    public void OnPlay()
    {
        StartCoroutine(StartGameFlow());
    }

    IEnumerator StartGameFlow()
    {
        yield return Fade(mainMenuPanel, 0f);
        if (hud) hud.SetActive(true);
        if (gameManager) gameManager.StartGame();
    }


    public void OnSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(true);
        StartCoroutine(Fade(mainMenuPanel, 0f));
    }

    public void OnBackFromSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        StartCoroutine(Fade(mainMenuPanel, 1f));
    }


    IEnumerator Fade(CanvasGroup g, float target)
    {
        if (!g) yield break;

        float start = g.alpha;
        float t = 0f;
        g.blocksRaycasts = true;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            g.alpha = Mathf.Lerp(start, target, t / fadeTime);
            yield return null;
        }

        g.alpha = target;
        bool shown = target > 0.99f;
        g.blocksRaycasts = shown;
        g.interactable = shown;
    }

    void ForceShow(CanvasGroup g)
    {
        if (!g) return;
        g.alpha = 1f; g.blocksRaycasts = true; g.interactable = true;
    }

    void ForceHide(CanvasGroup g)
    {
        if (!g) return;
        g.alpha = 0f; g.blocksRaycasts = false; g.interactable = false;
    }
}