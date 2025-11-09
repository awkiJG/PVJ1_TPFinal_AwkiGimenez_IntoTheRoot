using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerSensor : MonoBehaviour
{
    [SerializeField] private MosquitoController mosquitoController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Llama al Controlador para que maneje la lógica
            mosquitoController.Embestir(other.transform);
        }
    }
}