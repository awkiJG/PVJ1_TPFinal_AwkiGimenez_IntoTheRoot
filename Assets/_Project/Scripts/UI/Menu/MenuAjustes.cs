using UnityEngine;
using UnityEngine.UI;

public class MenuAjustes : UIPanel
{
    [Header("Referencias de Botones")]
    [SerializeField] private Button botonMusica;
    [SerializeField] private Image imgToggleMusica; // La imagen que cambia (hija del botón o el botón mismo)
    
    [SerializeField] private Button botonEfectos;
    [SerializeField] private Image imgToggleEfectos;

    [SerializeField] private Button botonAtras;
    [SerializeField] private GestorMenu gestor;

    [Header("Sprites de Estado")]
    [SerializeField] private Sprite spriteEncendido; // Arrastra tu imagen de "ON"
    [SerializeField] private Sprite spriteApagado;   // Arrastra tu imagen de "OFF"

    // Estados internos (por defecto true/encendido)
    private bool musicaActiva = true;
    private bool efectosActivos = true;

    private void Start()
    {
        // 1. Asignar los eventos a los botones de audio UNA sola vez al inicio
        botonMusica.onClick.AddListener(AlternarMusica);
        botonEfectos.onClick.AddListener(AlternarEfectos);

        // 2. Inicializar la visual (asegurarse que empiecen en ON)
        ActualizarVisuales();
    }

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        
        if (gestor == null)
        {
            Debug.LogError("GestorMenu no asignado en MenuAjustes");
            return;
        }

        // Configuración del botón Atrás (como lo tenías)
        botonAtras.onClick.RemoveAllListeners();
        botonAtras.onClick.AddListener(() => gestor.MostrarPanel(0));
        
        // Aseguramos que al abrirse se vea el estado correcto
        ActualizarVisuales();
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }

    // --- LÓGICA DE TOGGLE ---

    private void AlternarMusica()
    {
        // 1. Invertir el valor booleano (Si es true pasa a false, y viceversa)
        musicaActiva = !musicaActiva;

        // 2. Avisar al AudioManager (Singleton)
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic(musicaActiva);
        }

        // 3. Cambiar la imagen del botón
        ActualizarVisuales();
    }

    private void AlternarEfectos()
    {
        // 1. Invertir valor
        efectosActivos = !efectosActivos;

        // 2. Avisar al AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleSFX(efectosActivos);
        }

        // 3. Cambiar imagen
        ActualizarVisuales();
    }

    // Método auxiliar para actualizar las imágenes según el estado
    private void ActualizarVisuales()
    {
        // Operador ternario: (condicion) ? (si verdadero) : (si falso)
        imgToggleMusica.sprite = musicaActiva ? spriteEncendido : spriteApagado;
        imgToggleEfectos.sprite = efectosActivos ? spriteEncendido : spriteApagado;
    }
}