using UnityEngine;

public class MovimientoPj : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float velocidad = 5f;
    public float limIzq = -10f;
    public float limDer = 10f; 

    void Update()
    {
        // Obtener entrada del teclado (A/D o flechas izquierda/derecha)
        float movimientoX = Input.GetAxis("Horizontal"); 
        // "Horizontal" devuelve -1 (izquierda) a 1 (derecha)

        // Calcular el desplazamiento en función de la velocidad y el tiempo
        Vector3 desplazamiento = new Vector3(movimientoX, 0, 0) * velocidad * Time.deltaTime;

        // Aplicar el movimiento al objeto
        transform.Translate(desplazamiento, Space.World);
        // Limitar movimiento entre -10 y 10 en el eje X
        float limiteX = Mathf.Clamp(transform.position.x, limIzq, limDer);
        transform.position = new Vector3(limiteX, transform.position.y, transform.position.z);

    }
}

