using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [Header("Escribe aquí el link que se abrirá")]
    public string url = "https://tus-redes-sociales.com";

    public void OpenSocialLink()
    {
        Application.OpenURL(url);
    }
}
