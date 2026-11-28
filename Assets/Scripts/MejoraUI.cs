using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MejoraUI : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI actual;
    public TextMeshProUGUI mejora;
    public TextMeshProUGUI final;
    public Image color;
    public int comprarOCancelar = -1;
    [Header("Sonido de compra")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoMejora;
    [SerializeField] private AudioClip sonidoCancelar;


    public IEnumerator ActivarMejora(Casilla casilla)
    {
        comprarOCancelar = -1;
        color.color = casilla.color;
        nombre.text = casilla.nombreVideojuego;
        actual.text = "Edificar por: " + casilla.valorVenta * 2f;
        mejora.text = "Valor actual: " + casilla.valorVenta;
        final.text = "Valor al edificar: " + (int)(casilla.valorVenta * 1.5f);
        ui.SetActive(true);
        yield return new WaitUntil(() => comprarOCancelar > 0);
        ui.SetActive(false);
    }


    public void ComprarOCancelar(int a)
    {
        comprarOCancelar = a;

        if (a == 1)
        {

            audioSource.PlayOneShot(sonidoMejora);
        }
        else if (a == 2)
        {

            audioSource.PlayOneShot(sonidoCancelar);
        }
    }
}
