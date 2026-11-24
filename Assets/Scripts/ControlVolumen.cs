
using UnityEngine;
using UnityEngine.UI;

public class ControlVolumen : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSFX;

    private void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("No se encuentra el AudioManager.");
            return;
        }

        // Cargar los valores guardados
        sliderMaster.value = PlayerPrefs.GetFloat("VolumenMaster", 1f);
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusic", 1f);
        sliderSFX.value = PlayerPrefs.GetFloat("VolumenSFX", 1f);

        // Conectar los sliders al AudioManager
        sliderMaster.onValueChanged.AddListener(
            AudioManager.Instance.CambiarVolumenMaster);

        sliderMusica.onValueChanged.AddListener(
            AudioManager.Instance.CambiarVolumenMusica);

        sliderSFX.onValueChanged.AddListener(
            AudioManager.Instance.CambiarVolumenSFX);

        Debug.Log("Sliders conectados correctamente.");
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance == null) return;

        if (sliderMaster != null)
            sliderMaster.onValueChanged.RemoveListener(
                AudioManager.Instance.CambiarVolumenMaster);

        if (sliderMusica != null)
            sliderMusica.onValueChanged.RemoveListener(
                AudioManager.Instance.CambiarVolumenMusica);

        if (sliderSFX != null)
            sliderSFX.onValueChanged.RemoveListener(
                AudioManager.Instance.CambiarVolumenSFX);
    }
}
