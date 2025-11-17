using UnityEngine;
using System.Collections;

public class GusanoDialogo : MonoBehaviour
{
    [SerializeField] private GusanoFSM fsm;
    [SerializeField] private GameObject dialogo;
    [SerializeField] private SpriteRenderer panelDialogo;
    [SerializeField] private Sprite[] spritesSaludo;
    [SerializeField] private Sprite[] spritesGracias;

    public void MostrarDialogo()
    {
        if (fsm.CurrentState == GusanoFSM.GusanoState.DIALOGO_INICIAL)
        {
            StartCoroutine(DialogoCoroutine(spritesSaludo));
        }
        else if (fsm.CurrentState == GusanoFSM.GusanoState.DIALOGO_HUEVO)
        {
            StartCoroutine(DialogoCoroutine(spritesGracias));
        }
    }

    private IEnumerator DialogoCoroutine(Sprite[] sprites)
    {
        dialogo.SetActive(true);
        foreach (var sprite in sprites)
        {
            panelDialogo.sprite = sprite;
            yield return new WaitForSeconds(4f); // Esperar 4 segundos entre cada sprite
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