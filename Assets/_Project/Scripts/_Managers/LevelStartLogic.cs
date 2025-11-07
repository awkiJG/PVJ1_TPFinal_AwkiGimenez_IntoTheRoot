using UnityEngine;

public class LevelStartLogic : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Rigidbody2D playerRigidbody;
    void Start()
    {
        // 1. LEER EL MENSAJE
        if (playerStats.GetEntryPointID() == "Nivel_1_Arbol" || 
            playerStats.GetEntryPointID() == "Nivel_2_Raices")
        {
            // 2. APLICAR LA VELOCIDAD
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
        }
    }
}