using UnityEngine;
using System.Collections;

public class GusanoDialogo : MonoBehaviour
{
    [SerializeField] private GusanoFSM fsm;
    [SerializeField] private GameObject dialogo;
    [SerializeField] private SpriteRenderer panelDialogo;
    [SerializeField] private Sprite[] spritesSaludo;
    [SerializeField] private Sprite[] spritesGracias;
    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;

    public void MostrarDialogo()
    {
        if (fsm.CurrentState == GusanoFSM.GusanoState.DIALOGO_INICIAL)
        {
            StartCoroutine(DialogoCoroutine(spritesSaludo, audio1));
        }
        else if (fsm.CurrentState == GusanoFSM.GusanoState.DIALOGO_HUEVO)
        {
            StartCoroutine(DialogoCoroutine(spritesGracias, audio2));
        }
    }

    private IEnumerator DialogoCoroutine(Sprite[] sprites, AudioClip audio)
    {
        dialogo.SetActive(true);
        AudioManager.Instance.PlaySFX(audio);
        foreach (var sprite in sprites)
        {
            panelDialogo.sprite = sprite;
            yield return new WaitForSeconds(5.5f); // Esperar 5.5 segundos entre cada sprite
        }

        fsm.ChangeState(
            fsm.CurrentState == GusanoFSM.GusanoState.DIALOGO_INICIAL ?
            GusanoFSM.GusanoState.ESPERANDO_ACEPTAR_MISION :
            GusanoFSM.GusanoState.ESPERANDO_ENTREGAR_HUEVO
        );
    }

    public GameObject GetDialogoObject()
    {
        return dialogo;
    }
}