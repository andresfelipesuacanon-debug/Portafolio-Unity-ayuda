using UnityEngine;
using UnityEngine.SceneManagement;

public class SeguirPersonaje : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public Transform jugador;

    [Header("Ajustes de cámara")]
    public float suavizado = 5f; // Qué tan suave sigue la cámara
    public float offsetX = 0f;   // Desplazamiento en X, por si quieres que el jugador no esté centrado

    [Header("Límites del movimiento en X")]
    public float limiteIzquierdo = -10f;
    public float limiteDerecho = 10f;

    private static SeguirPersonaje instancia; // Control de persistencia
    private Vector3 posicionDeseada;

    void Awake()
    {
        // Evita duplicados y mantiene la cámara entre escenas
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Buscar jugador al iniciar
        BuscarJugador();

        // Escuchar cuando cambie de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void LateUpdate()
    {
        if (jugador == null)
            return;

        // Calcular la posición deseada de la cámara
        posicionDeseada = new Vector3(jugador.position.x + offsetX, transform.position.y, transform.position.z);

        // Limitar la posición dentro de los rangos establecidos
        posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, limiteIzquierdo, limiteDerecho);

        // Mover la cámara suavemente hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
    }

    void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        // Al cargar una nueva escena, buscar al jugador persistente
        BuscarJugador();
    }

    void BuscarJugador()
    {
        if (jugador == null)
        {
            GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
            if (jugadorGO != null)
                jugador = jugadorGO.transform;
        }
    }

    private void OnDestroy()
    {
        // Evita referencias colgantes
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
