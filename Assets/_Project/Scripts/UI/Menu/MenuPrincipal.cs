using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuPrincipal : UIPanel
{
    
    [SerializeField] private Button botonJugar;
    [SerializeField] private Button botonNiveles;
    [SerializeField] private Button botonAjustes;
    [SerializeField] private Button botonCreditos;
    [SerializeField] private Button botonSalir;
    [SerializeField] private GestorMenu gestor;
    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestor == null)
        {
            Debug.LogError("GestorMenu no asignado en MenuPrincipal");
            return;
        }
        botonJugar.onClick.RemoveAllListeners();
        botonJugar.onClick.AddListener(() => SceneManager.LoadScene("Nivel_1_Arbol"));

        botonNiveles.onClick.RemoveAllListeners();
        botonNiveles.onClick.AddListener(() => gestor.MostrarPanel(1));

        botonAjustes.onClick.RemoveAllListeners();
        botonAjustes.onClick.AddListener(() => gestor.MostrarPanel(2));

        botonCreditos.onClick.RemoveAllListeners();
        botonCreditos.onClick.AddListener(() => gestor.MostrarPanel(3));

        botonSalir.onClick.RemoveAllListeners();
        botonSalir.onClick.AddListener(() => gestor.Salir());

    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
