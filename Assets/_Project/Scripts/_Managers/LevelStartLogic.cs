using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartLogic : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Rigidbody2D playerRigidbody;
    void Start()
    {
        string nivelAcual = SceneManager.GetActiveScene().name.ToString();

        if (nivelAcual == "Nivel_1_Arbol")
        {
            playerStats.ResetToDefaults();
        }
        else if (nivelAcual == "Nivel_2_Raices")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMap(false);
            playerStats.SetCurrentSap(0);
        }
        else if (nivelAcual == "Nivel_3_Mosquitos")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMap(false);
            playerStats.SetHasMosquitoEgg(false);
            playerStats.SetMaxSap(playerStats.GetCurrentSap());
        }
        else if (nivelAcual == "Nivel_4_Purificacion")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMosquitoEgg(false);
            playerStats.SetCurrentSap(playerStats.GetMaxSap());
        }
    }
}