using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNiveles : UIPanel
{
    [SerializeField] private Button botonNivel1;
    [SerializeField] private Button botonNivel2;
    [SerializeField] private Button botonNivel3;
    [SerializeField] private Button botonNivel4;
    [SerializeField] private Button botonAtras;
    [SerializeField] private GestorMenu gestor;
    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestor == null)
        {
            Debug.LogError("GestorMenu no asignado en MenuNiveles");
            return;
        }
        botonNivel1.onClick.RemoveAllListeners();
        botonNivel1.onClick.AddListener(() => SceneManager.LoadScene("Nivel_1_Arbol"));

        botonNivel2.onClick.RemoveAllListeners();
        botonNivel2.onClick.AddListener(() => SceneManager.LoadScene("Nivel_2_Raices"));

        botonNivel3.onClick.RemoveAllListeners();
        botonNivel3.onClick.AddListener(() => SceneManager.LoadScene("Nivel_3_Mosquitos"));

        botonNivel4.onClick.RemoveAllListeners();
        botonNivel4.onClick.AddListener(() => SceneManager.LoadScene("Nivel_4_Purificacion"));

        botonAtras.onClick.RemoveAllListeners();
        botonAtras.onClick.AddListener(() => gestor.MostrarPanel(0));
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        // 3. Traer el dato guardado
        int highestLevel = PlayerPrefs.GetInt("HighestLevelReached", 1);

        // 4. Aplicar la lógica condicional

        // (Asumimos que el Nivel 1 siempre está disponible)
        botonNivel1.interactable = true;

        // El Nivel 2 solo es interactuable si el progreso es 2 o más
        botonNivel2.interactable = (highestLevel >= 2);

        // El Nivel 3 solo si el progreso es 3 o más
        botonNivel3.interactable = (highestLevel >= 3);

        // El Nivel 4 solo si el progreso es 4 o más
        botonNivel4.interactable = (highestLevel >= 4);
    }
    private void Update()
    {
        // Presiona F1 para resetear el progreso
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("TEST: Progreso reseteado a Nivel 1.");
            PlayerPrefs.SetInt("HighestLevelReached", 1);
            PlayerPrefs.Save(); // Guardar el cambio
            OnEnable(); // Actualizar los botones al instante
        }

        // Presiona F2 para desbloquear el Nivel 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("TEST: Desbloqueando Nivel 2.");
            PlayerPrefs.SetInt("HighestLevelReached", 2);
            PlayerPrefs.Save();
            OnEnable();
        }

        // Presiona F3 para desbloquear el Nivel 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("TEST: Desbloqueando Nivel 3.");
            PlayerPrefs.SetInt("HighestLevelReached", 3);
            PlayerPrefs.Save();
            OnEnable();
        }

        // Presiona F4 para desbloquear el Nivel 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("TEST: Desbloqueando Nivel 4.");
            PlayerPrefs.SetInt("HighestLevelReached", 4);
            PlayerPrefs.Save();
            OnEnable();
        }
    }
}
