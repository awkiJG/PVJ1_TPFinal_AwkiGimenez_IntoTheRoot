using UnityEngine;
using System.Collections;
public class StrokeController : MonoBehaviour
{
    private float velocidad = 2f;
    [Range(-1f, 1f)]
    [SerializeField] private float direccion;
    [SerializeField] private Rigidbody2D rb;
    [Header("Mensaje")]
    [SerializeField] private GameObject dialogo;
    [SerializeField] private SpriteRenderer panelDialogo;
    [SerializeField] private Sprite[] spritesMensaje;
    [SerializeField] private AudioClip audio;

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
                direccion * velocidad,
                rb.linearVelocity.y);  
    }

    public void SetDireccion(float dir)
    {
        direccion = dir;
    }

    public IEnumerator MostrarMensajeCoroutine()
    {
        AudioManager.Instance.PlaySFX(audio);
        dialogo.SetActive(true);
        
        foreach (var sprite in spritesMensaje)
        {
            panelDialogo.sprite = sprite;
            yield return new WaitForSeconds(5f); // Esperar 4 segundos entre cada sprite
        }
        dialogo.SetActive(false);
    }
}
