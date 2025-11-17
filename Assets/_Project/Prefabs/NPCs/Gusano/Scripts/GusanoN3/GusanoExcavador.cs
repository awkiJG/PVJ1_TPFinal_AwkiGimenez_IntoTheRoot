using UnityEngine;
using System.Collections;

public class GusanoExcavador : MonoBehaviour
{
    [SerializeField] private GusanoFSM fsm;
    private Vector3 startPosition;
    [SerializeField] private Animator animator;

    public void IniciarMovimiento(int direccion, float distancia)
    {
        StartCoroutine(MoverCorrutine(direccion, distancia));
    }

    private IEnumerator MoverCorrutine(int direccion, float distancia)
    {
        animator.SetBool("cavando", true);
        if (fsm.CurrentState == GusanoFSM.GusanoState.EXCAVANDO_TUNEL_2) transform.rotation = new Quaternion(0, 180, 0, 0);

        startPosition = transform.position;
        // 2. Calcular el destino final basado en la dirección  
        Vector3 destinoFinal = new Vector3(
            startPosition.x + direccion * distancia, // Coordenada X fuera de pantalla
            startPosition.y,
            startPosition.z
        );
        // 3. Mover el Gusano (y su hija, la 'tapaTunel')
        while (Vector3.Distance(transform.position, destinoFinal) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinoFinal,
                5f * Time.deltaTime
            );
            yield return null; // Esperar al próximo frame
        }
        if (fsm.CurrentState == GusanoFSM.GusanoState.EXCAVANDO_TUNEL_1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            while (Vector3.Distance(transform.position, startPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    startPosition,
                    5f * Time.deltaTime
                );
                yield return null; // Esperar al próximo frame
            }
            animator.SetBool("cavando", false);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            fsm.ChangeState(GusanoFSM.GusanoState.ESPERANDO_HUEVO);
        }else
        {
            fsm.ChangeState(GusanoFSM.GusanoState.COMPLETADO);
        }
    }
}
