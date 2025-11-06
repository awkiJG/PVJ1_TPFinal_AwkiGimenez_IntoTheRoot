using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFader : MonoBehaviour
{
    // Image que se usará para el fade (puede asignarse desde el Inspector)
    [SerializeField] private Image fadeImage;
    // Inicia el fade a negro en 0.5 segundos
    public void FadeToBlack()
    {
        // Iniciar corrutina que hace el desvanecimiento
        StartCoroutine(FadeToBlackCoroutine(0.5f));
    }
    // Corrutina que hace el fade progresivo usando tiempo no-escalado
    private IEnumerator FadeToBlackCoroutine(float duration)
    {
        // Preparar colores inicial y objetivo
        Color startColor = fadeImage.color;
        Color targetColor = Color.black;
        float startAlpha = startColor.a;
        float elapsed = 0f;
        // Usar unscaledDeltaTime para que el fade funcione aunque Time.timeScale == 0
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float alpha = Mathf.Lerp(startAlpha, 1f, t);
            // Aplicar color negro con alpha interpolado
            fadeImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }
        // Asegurar opacidad final completa
        fadeImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, 1f);
    }
}
