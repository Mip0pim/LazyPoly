
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    private const string PARAMETRO_SFX = "VolumenSFX";
    private const string PARAMETRO_MUSIC = "VolumenMusic";
    private const string PARAMETRO_MASTER = "VolumenMaster";

    private void Awake()
    {
        Debug.Log("AudioManager Awake ejecutado");

        if (Instance != null && Instance != this)
        {
            Debug.Log("AudioManager duplicado, destruyendo el nuevo");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("AudioManager persistente creado");

        float sfx = PlayerPrefs.GetFloat("VolumenSFX", 1f);
        float music = PlayerPrefs.GetFloat("VolumenMusic", 1f);
        float master = PlayerPrefs.GetFloat("VolumenMaster", 1f);

        CambiarVolumenSFX(sfx);
        CambiarVolumenMusica(music);
        CambiarVolumenMaster(master);
    }

    public void CambiarVolumenSFX(float volumen)
    {
        AplicarVolumen(PARAMETRO_SFX, "VolumenSFX", volumen);
    }

    public void CambiarVolumenMusica(float volumen)
    {
        AplicarVolumen(PARAMETRO_MUSIC, "VolumenMusic", volumen);
    }

    public void CambiarVolumenMaster(float volumen)
    {
        AplicarVolumen(PARAMETRO_MASTER, "VolumenMaster", volumen);
    }

    private void AplicarVolumen(string parametro, string clave, float volumen)
    {
        volumen = Mathf.Clamp01(volumen);

        float db = volumen <= 0.0001f
            ? -80f
            : Mathf.Log10(volumen) * 20f;

        if (audioMixer == null)
        {
            Debug.LogError("No hay ningún AudioMixer asignado.");
            return;
        }

        bool aplicado = audioMixer.SetFloat(parametro, db);

        if (!aplicado)
        {
            Debug.LogError("No se ha podido modificar el parámetro: " + parametro);
            return;
        }

        PlayerPrefs.SetFloat(clave, volumen);
        PlayerPrefs.Save();

        Debug.Log(parametro + " cambiado a " + volumen + " (" + db + " dB)");
    }
}
