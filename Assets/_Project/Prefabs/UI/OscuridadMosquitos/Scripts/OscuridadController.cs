using UnityEngine;

public class OscuridadController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Oscuridad");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Oscuridad");
        }
    }
}
