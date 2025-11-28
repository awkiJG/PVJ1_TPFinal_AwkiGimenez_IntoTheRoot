using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private AudioClip narracion;
    [SerializeField] private AudioClip music;
    [SerializeField] private Image visor;
    [SerializeField] private Sprite[] imagenes;
    [SerializeField] private Animator animatorFader;


    void Start()
    {
        StartCoroutine(CinematicaInicio());
    }

    private IEnumerator CinematicaInicio()
    {
        AudioManager.Instance.PlayMusic(music);
        visor.sprite = imagenes[0];
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySFX(narracion);
        yield return new WaitForSeconds(20f);
        animatorFader.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        visor.sprite = imagenes[1];
        animatorFader.SetTrigger("FadeOut");
        yield return new WaitForSeconds(18f);
        animatorFader.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        visor.sprite = imagenes[2];
        animatorFader.SetTrigger("FadeOut");
        yield return new WaitForSeconds(19f);
        animatorFader.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        visor.sprite = imagenes[3];
        animatorFader.SetTrigger("FadeOut");
        yield return new WaitForSeconds(20f);
        animatorFader.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        visor.sprite = imagenes[4];
        animatorFader.SetTrigger("FadeOut");
        yield return new WaitForSeconds(27f);
        animatorFader.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel_1_Arbol");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
