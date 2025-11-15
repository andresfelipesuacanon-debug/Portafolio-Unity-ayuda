using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public Transform jugador;

    [Header("UI de interacción (nombre exacto en la escena por si hay que buscarlos)")]
    public GameObject indicadorEcama;
    public GameObject indicadorEtelevisor;
    public GameObject indicadorEfuego;
    public GameObject indicadorEcajas;

    [Header("Configuración de interacción")]
    public float distanciaInteraccion = 2f;

    [Header("Escenas destino")]
    public string escenaCama = "Escena2";
    public string escenaTelevisor = "Escena3";
    public string escenaFuego = "Escena4";
    public string escenaCajas = "Escena5";

    [Header("Nombres (opcional) para re-buscar objetos de la escena automáticamente")]
    public string nombreCama = "Cama";
    public string nombreTelevisor = "Televisor";
    public string nombreFuego = "Fuego";
    public string nombreCajas = "Cajas";

    // referencias internas que apuntan a los objetos de la escena
    private Transform cama;
    private Transform televisor;
    private Transform fuego;
    private Transform cajas;

    private static CambioDeEscena instancia;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        // Si este objeto se está marcando como DontDestroyOnLoad en otro momento,
        // mantenerlo está bien; de todos modos re-asignaremos las referencias al cargar escena.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ReasignarReferenciasSiHaceFalta();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReasignarReferenciasSiHaceFalta();
    }

    private void ReasignarReferenciasSiHaceFalta()
    {
        // Si no hay jugador, intentar buscar por tag Player
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        // Si las referencias de interactuables son null, buscarlas por nombre en la escena
        if (cama == null)
        {
            var go = GameObject.Find(nombreCama);
            if (go != null) cama = go.transform;
        }

        if (televisor == null)
        {
            var go = GameObject.Find(nombreTelevisor);
            if (go != null) televisor = go.transform;
        }

        if (fuego == null)
        {
            var go = GameObject.Find(nombreFuego);
            if (go != null) fuego = go.transform;
        }

        if (cajas == null)
        {
            var go = GameObject.Find(nombreCajas);
            if (go != null) cajas = go.transform;
        }

        // Si los indicadores quedaron sin asignar (por ejemplo están en el Canvas de la nueva escena), buscar por nombre
        if (indicadorEcama == null) indicadorEcama = GameObject.Find("IndicadorEcama");
        if (indicadorEtelevisor == null) indicadorEtelevisor = GameObject.Find("IndicadorEtelevisor");
        if (indicadorEfuego == null) indicadorEfuego = GameObject.Find("IndicadorEfuego");
        if (indicadorEcajas == null) indicadorEcajas = GameObject.Find("IndicadorEcajas");

        // Opcional: asegurar que empiecen desactivados
        if (indicadorEcama != null) indicadorEcama.SetActive(false);
        if (indicadorEtelevisor != null) indicadorEtelevisor.SetActive(false);
        if (indicadorEfuego != null) indicadorEfuego.SetActive(false);
        if (indicadorEcajas != null) indicadorEcajas.SetActive(false);
    }

    void Update()
    {
        if (jugador == null) return;

        RevisarInteraccion(cama, indicadorEcama, escenaCama);
        RevisarInteraccion(televisor, indicadorEtelevisor, escenaTelevisor);
        RevisarInteraccion(fuego, indicadorEfuego, escenaFuego);
        RevisarInteraccion(cajas, indicadorEcajas, escenaCajas);
    }

    void RevisarInteraccion(Transform objeto, GameObject indicador, string escena)
    {
        if (objeto == null || indicador == null) return;

        float distancia = Vector3.Distance(objeto.position, jugador.position);
        bool enRango = distancia <= distanciaInteraccion;

        indicador.SetActive(enRango);

        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(escena);
        }
    }
}
