using UnityEngine;

public class LevelStartLogic : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Rigidbody2D playerRigidbody;
    void Start()
    {
        // 1. LEER EL MENSAJE
        if (playerStats.GetEntryPointID() == "Nivel_1_Arbol")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetSap(0);
        }
        else if (playerStats.GetEntryPointID() == "Nivel_2_Raices")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMap(false);
        }
        else if (playerStats.GetEntryPointID() == "Nivel_3_Mosquitos")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMap(false);

        }
    }
}