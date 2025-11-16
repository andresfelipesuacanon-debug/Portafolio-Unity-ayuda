using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [Header("URL a abrir")]
    public string url;

    public void OpenSocialLink()
    {
        Application.OpenURL(url);
    }
}
