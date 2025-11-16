using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelZoneManager : MonoBehaviour
{
    [System.Serializable]
    public class PanelZone
    {
        public string panelName;          // El nombre EXACTO del panel en la escena
        public float positionX;           // Coordenada X donde aparece
        public float tolerancia = 0.5f;   // Qué tan cerca debe estar el jugador
        public string sceneToLoad;        // Nombre de la escena al presionar E
        [HideInInspector] public GameObject panelRef; // Referencia automática
    }

    public PanelZone[] zonas; 
    private Transform jugador; 

    void Start()
    {
        // Buscar jugador automáticamente
        jugador = GameObject.FindWithTag("Player")?.transform;
        if (jugador == null)
            Debug.LogError("No encontré el jugador. Asegúrate de que tenga el tag 'Player'.");

        // Encontrar los paneles automáticamente
        foreach (var zona in zonas)
        {
            zona.panelRef = GameObject.FindWithTag(zona.panelName);

            if (zona.panelRef == null)
                Debug.LogError("No encontré el panel con nombre: " + zona.panelName);

            zona.panelRef.SetActive(false); // siempre inicia apagado
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float x = jugador.position.x;

        foreach (var zona in zonas)
        {
            bool estaCerca = Mathf.Abs(x - zona.positionX) <= zona.tolerancia;

            // Mostrar u ocultar
            zona.panelRef.SetActive(estaCerca);

            // Tecla de acción
            if (estaCerca && Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(zona.sceneToLoad);
            }
        }
    }
}
