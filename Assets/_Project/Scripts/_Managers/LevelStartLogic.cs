using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartLogic : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private AudioClip[] musicas;
    void Start()
    {
        GameManager.RequestPauseState(false);
        GameManager.RequestInputLock(false);
        string nivelAcual = SceneManager.GetActiveScene().name.ToString();

        if (nivelAcual == "Nivel_1_Arbol")
        {
            playerStats.ResetToDefaults();
            AudioManager.Instance.PlayMusic(musicas[0]);
        }
        else if (nivelAcual == "Nivel_2_Raices")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMap(false);
            playerStats.SetCurrentSap(0);
            AudioManager.Instance.PlayMusic(musicas[1]);
        }
        else if (nivelAcual == "Nivel_3_Mosquitos")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMap(false);
            playerStats.SetHasMosquitoEgg(false);
            playerStats.SetMaxSap(playerStats.GetCurrentSap());
            AudioManager.Instance.PlayMusic(musicas[2]);
        }
        else if (nivelAcual == "Nivel_4_Purificacion")
        {
            playerRigidbody.linearVelocity = new Vector2(0f, -30f);
            playerStats.SetHasMosquitoEgg(false);
            playerStats.SetCurrentSap(playerStats.GetMaxSap());
            AudioManager.Instance.PlayMusic(musicas[3]);
        }
        if (nivelAcual == "MainMenu")
        {
            playerStats.ResetToDefaults();
            GameManager.RequestInputLock(false);
            GameManager.RequestPauseState(false);
        }
    }
}