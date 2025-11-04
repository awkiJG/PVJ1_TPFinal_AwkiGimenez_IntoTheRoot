using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private float direccion;

    void Update()
    {
        // Capturar input horizontal (-1 a 1)
        direccion = Input.GetAxis("Horizontal");

        // Aplicar movimiento horizontal usando transform
        if (direccion != 0)
        {
            transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);
        }

        // Voltear el sprite según la dirección del movimiento
        if (direccion != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
        }
    }
    // Método público para que otros scripts puedan consultar la dirección
    public float GetDireccion()
    {
        return direccion;
    }
}
