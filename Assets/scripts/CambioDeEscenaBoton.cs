using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using UnityEngine.UI;              // Necesario para usar botones UI

public class CambioDeEscenaBoton : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena; // Escribe aquí el nombre exacto de la escena

    // Método que se ejecuta cuando se presiona el botón
    public void CambiarEscena()
    {
        // Verifica si la escena está incluida en el Build Settings
        if (Application.CanStreamedLevelBeLoaded(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogError($"La escena '{nombreEscena}' no está en los Build Profiles o el nombre no coincide.");
        }
    }
}
