using UnityEngine;
using System.Collections;

public class RootsController : MonoBehaviour
{
    [SerializeField] private RootAttack[] roots;
    void Start()
    {
        StartCoroutine(ActivateAttack());
    }
    public IEnumerator ActivateAttack()
    {
        yield return new WaitForSeconds(2f);
        roots[0].ActivateAttack(-25f, 25f, 4f);
        yield return new WaitForSeconds(1f);
        roots[1].ActivateAttack(-30f, 25f, 2f);
        yield return new WaitForSeconds(1f);
        roots[2].ActivateAttack(-20f, 40f, 2f);
        yield return new WaitForSeconds(0.2f);
        roots[3].ActivateAttack(-20f, 40f, 2f);
        yield return new WaitForSeconds(0.2f);
        roots[4].ActivateAttack(-20f, 40f, 2f);
        yield return new WaitForSeconds(0.2f);
        roots[5].ActivateAttack(-20f, 40f, 2f);

    
    }
}
