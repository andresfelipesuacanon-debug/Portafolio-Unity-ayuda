using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorPersistente : MonoBehaviour
{
    private static JugadorPersistente instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Mantiene el jugador entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados si vuelves a la escena original
        }
    }
}
