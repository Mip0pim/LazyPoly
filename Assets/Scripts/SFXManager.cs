
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSourceSFX;

    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidoCarcel;
    [SerializeField] private AudioClip sonidoDados;
    [SerializeField] private AudioClip sonidoCompra;
    [SerializeField] private AudioClip sonidoVenta;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ReproducirSonidoCarcel()
    {
        ReproducirSonido(sonidoCarcel);
    }

    public void ReproducirSonidoDados()
    {
        ReproducirSonido(sonidoDados);
    }

    public void ReproducirSonidoCompra()
    {
        ReproducirSonido(sonidoCompra);
    }

    public void ReproducirSonidoVenta()
    {
        ReproducirSonido(sonidoVenta);
    }

    public void ReproducirSonido(AudioClip sonido)
    {
        if (audioSourceSFX != null && sonido != null)
        {
            audioSourceSFX.PlayOneShot(sonido);
        }
    }
}
