using UnityEngine;
using System.Collections;

public class FadePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component missing on this GameObject.");
            return;
        }

        StartCoroutine(FadeOutPanel(3f)); // Change the duration as needed
    }

    IEnumerator FadeOutPanel(float duration)
    {
        float startAlpha = 1f; // Fully opaque
        float endAlpha = 0f;   // Fully transparent
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        // Ensure it's fully transparent at the end
        canvasGroup.alpha = endAlpha;
    }
}
