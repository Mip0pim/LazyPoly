using UnityEngine;
using UnityEngine.UI;

public class CambiarMusica : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Slider slider;
    private AudioSource musica;

    private void Start()
    {
        musica = GameObject.FindWithTag("Musica").GetComponent<AudioSource>();
        slider.value = musica.volume;
    }

    public void SetVolume(float valor)
    {
        musica.volume = valor;
    }
}
