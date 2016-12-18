using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int currentSceneID;
    public int nextSceneID;
    public float fadeTime;
    public Image fadePanel;

    private void Start()
    {
        StartCoroutine(FadeInSequence());
    }

    public void LoadNextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutSequence(nextSceneID));
    }

    public void ReloadLevel()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutSequence(currentSceneID));
    }

    IEnumerator FadeInSequence()
    {
        Color tempColor = fadePanel.color;
        float startValue = tempColor.a;
        float targetValue = 0.0f;

        for (float i = 0; i < 1; i += Time.deltaTime / fadeTime)
        {
            tempColor.a = Mathf.Lerp(startValue, targetValue, i);
            fadePanel.color = tempColor;
            yield return null;
        }

        tempColor.a = targetValue;
        fadePanel.color = tempColor;
    }

    IEnumerator FadeOutSequence(int sceneID)
    {
        Color tempColor = fadePanel.color;
        float startValue = tempColor.a;
        float targetValue = 1.0f;

        for (float i = 0; i < 1; i += Time.deltaTime / fadeTime)
        {
            tempColor.a = Mathf.Lerp(startValue, targetValue, i);
            fadePanel.color = tempColor;
            yield return null;
        }

        tempColor.a = targetValue;
        fadePanel.color = tempColor;

        SceneManager.LoadScene(sceneID);
    }
}
