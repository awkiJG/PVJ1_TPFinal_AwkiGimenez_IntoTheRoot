using UnityEngine;

public class ExplosiveBomb : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private int spikeCount;
    [SerializeField] private float force;
    [SerializeField] private float scale;

    public void Initialize(int count, float force, float scale)
    {
        spikeCount = count;
        this.force = force;
        this.scale = scale;
        transform.localScale = new Vector3(scale, scale, scale);
    
    }

    private void Explode()
    {
        float angleStep = 360f / spikeCount;

        // Bucle de Spawneo de Espinas
        for (int i = 0; i < spikeCount; i++)
        {
            // Calcular el ángulo actual
            float currentAngle = angleStep * i;
            
            // Convertir el ángulo (grados) a un vector de dirección 2D (radianes)
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

            // Instanciar el prefab de la espina
            GameObject spikeGO = Instantiate(spikePrefab, transform.position, Quaternion.identity);
            
            // Aplicar la escala a la nueva espina
            spikeGO.transform.localScale = new Vector3(scale, scale, scale);

            // Obtener el script de la espina y pasarle los datos
            SpikeHazard spikeScript = spikeGO.GetComponent<SpikeHazard>();
            if (spikeScript != null)
            {
                spikeScript.Initialize(direction, force);
            }
            else
            {
                Debug.LogWarning("El Prefab 'SpikeHazard' no tiene el script 'SpikeHazard.cs'");
            }
        }
        Destroy(gameObject);
    }
        
    void Update()
    {
        transform.Rotate(0, 0, 300 * Time.deltaTime);

        // 3. Chequear si ha llegado a la altura de explosión
        if (transform.position.y <= -254)
        {
            Explode();
        }
    }
}
