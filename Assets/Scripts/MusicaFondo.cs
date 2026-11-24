
using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    private static MusicaFondo instancia;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}
