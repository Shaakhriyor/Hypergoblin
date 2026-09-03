using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInWithDelay : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private Image targetImage;
    [SerializeField] private float delayDuration = 2.0f; // Time to wait before fading
    [SerializeField] private float fadeDuration = 1.5f;  // How long the fade takes

    private void Start()
    {
        if (targetImage != null)
        {
            // Start the coroutine when the game begins
            StartCoroutine(FadeInSequence());
        }
        else
        {
            Debug.LogError("Target Image is not assigned!");
        }
    }

    private IEnumerator FadeInSequence()
    {
        // 1. Set the image to completely transparent at the start
        Color originalColor = targetImage.color;
        targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // 2. Wait for the specified delay time
        yield return new WaitForSeconds(delayDuration);

        // 3. Fade in over time
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the current alpha based on elapsed time
            float newAlpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            // Apply the new alpha to the image
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null; // Wait for the next frame
        }

        // Ensure it is perfectly fully visible at the end
        targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}