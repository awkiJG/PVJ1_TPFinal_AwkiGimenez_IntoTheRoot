using UnityEngine;

public class MapController : MonoBehaviour
{    
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject mapObject; 
    [SerializeField] private GameObject _HUDPanel; 
    [SerializeField] private PauseController pauseController;
    private bool isMapOpen = false;
    private void Start()
    {
        // Asegurarse que el mapa esté cerrado al iniciar
        if (mapObject != null)
        {
            mapObject.SetActive(false);
        }
        isMapOpen = false;
    }

    void Update()
    {
        // Escucha la tecla 'M' 
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Llama a la función de alternar y no esta en pausa
            if (!pauseController.IsPaused())
            {
                ToggleMap();

            }
        }
    }
        
    public void ToggleMap()
    {
        // 1. Condición: ¿El jugador tiene el mapa?
        // Si no tiene el asset o no tiene el mapa, no hacer nada.
        if (playerStats == null || !playerStats.HasMap())
        {
            return; 
        }
        // 2. Invertir el estado (de abierto a cerrado, o viceversa)
        isMapOpen = !isMapOpen;

        // 3. Solicitar Pausa/Despausa al GameManager
        GameManager.RequestPauseState(isMapOpen);
        // 4. Activar/Desactivar Paneles
        if (mapObject != null)
        {
            mapObject.SetActive(isMapOpen);
        }
        if (_HUDPanel != null)
        {
            // Oculta el HUD si el mapa está abierto (y viceversa)
            _HUDPanel.SetActive(!isMapOpen);
        }
    }
    public bool IsMapOpen()
    {
        return isMapOpen;
    }
}