using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject interactPrompt;
    private bool jugadorEnRango = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            interactPrompt.SetActive(false);
        }
    }
    
    void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            playerStats.SetHasMosquitoEgg(true);
            GameObject.Destroy(this.gameObject);
        }
    }
}
