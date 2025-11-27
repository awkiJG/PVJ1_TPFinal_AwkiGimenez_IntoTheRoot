using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private MapController mapController;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mapController != null && mapController.IsMapOpen())
            {
                // Si el mapa está abierto, cerrarlo en lugar de pausar el juego
                mapController.ToggleMap();
            }
            else
            {
                // Alternar el estado de pausa normal
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        GameManager.RequestPauseState(isPaused);
        GameManager.RequestInputLock(isPaused);
        if (isPaused)
        {
            // Mostrar menú de pausa y ocultar HUD
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
            }
            if (HUD != null)
            {
                HUD.SetActive(false);
            }
        }
        else
        {
            // Ocultar menú de pausa y mostrar HUD
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
            if (HUD != null)
            {
                HUD.SetActive(true);
            }
        }  
    }
    public bool IsPaused()
    {
        return isPaused;
    }
}
