using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMainPanel : UIPanel
{
    [SerializeField] private PauseController pauseCon;
    [SerializeField] private Button botonContinuar;
    [SerializeField] private Button botonAjustes;
    [SerializeField] private Button botonMenu;
    [SerializeField] private GestorMenu gestor;

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestor == null)
        {
            Debug.LogError("GestorMenu no asignado en PauseMainPanel");
            return;
        }
        botonContinuar.onClick.RemoveAllListeners();
        botonContinuar.onClick.AddListener(() => pauseCon.TogglePause());

        botonAjustes.onClick.RemoveAllListeners();
        botonAjustes.onClick.AddListener(() => gestor.MostrarPanel(1));

        botonMenu.onClick.RemoveAllListeners();
        botonMenu.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
