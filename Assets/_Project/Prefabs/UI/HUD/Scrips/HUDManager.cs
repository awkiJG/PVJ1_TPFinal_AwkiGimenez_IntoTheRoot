using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // --- CONEXIÓN A DATOS ---
    
    [Header("Conexión de Datos")]
    [SerializeField] private PlayerStats playerStats;

    // --- REFERENCIAS VISUALES ---

    [Header("Visuales de Vida")]
    [SerializeField] private Image livesDisplayImage;
    [SerializeField] private Sprite[] livesSprites;
    
    [Header("Visuales de Savia y Mapa")]
    [SerializeField] private TextMeshProUGUI sapCounterText;
    [SerializeField] private GameObject mapIconObject;

    // ---  LÓGICA DE ACTUALIZACIÓN ---
    private void Update()
    {
        UpdateLives();
        UpdateSap();
        UpdateMap();
    }

    private void UpdateLives()
    {
        // 1. Obtener el valor actual de forma encapsulada (Getter)
        int currentLives = playerStats.GetCurrentLives();
        
        // 2. Determinar el índice correcto del array de sprites
        // Si tienes 4 sprites (3, 2, 1, 0 vidas), el índice va de 0 a 3.
        // Índice 0 = 3 vidas (livesSprites[0])
        // Índice 3 = 0 vidas (livesSprites[3])
        int maxLives = playerStats.GetMaxLives(); 
        int spriteIndex = maxLives - currentLives;

        // Aseguramos que el índice no esté fuera del límite del array
        spriteIndex = Mathf.Clamp(spriteIndex, 0, livesSprites.Length - 1);
        
        // 3. Asignar el sprite
        if (livesDisplayImage != null && livesSprites.Length > 0)
        {
            livesDisplayImage.sprite = livesSprites[spriteIndex];
        }
    }

    private void UpdateSap()
    {
        // 1. Obtener el valor actual de forma encapsulada (Getter)
        int currentSap = playerStats.GetCurrentSap();
        
        // 2. Asignar al componente de texto
        if (sapCounterText != null)
        {
            sapCounterText.text = currentSap.ToString();
        }
    }

    private void UpdateMap()
    {
        // 1. Obtener el estado del mapa de forma encapsulada (Getter)
        bool hasMap = playerStats.HasMap();
        
        // 2. Activar o desactivar el GameObject del ícono
        if (mapIconObject != null)
        {
            // Solo actualizamos si el estado es diferente para evitar llamadas innecesarias
            if (mapIconObject.activeSelf != hasMap)
            {
                mapIconObject.SetActive(hasMap);
            }
        }
    }
}