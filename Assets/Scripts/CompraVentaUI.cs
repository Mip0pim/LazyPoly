using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompraVentaUI : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI compra;
    public TextMeshProUGUI venta;
    public Image color;
    public int comprarOCancelar = -1;

    public IEnumerator ActivarCompra(Casilla casilla)
    {
        comprarOCancelar = -1;
        color.color = casilla.color;
        nombre.text = casilla.nombreVideojuego;
        venta.text = "Valor de venta: " + casilla.valorVenta;
        compra.text = "Valor de compra: " + casilla.coste;
        ui.SetActive(true);
        yield return new WaitUntil(()=> comprarOCancelar > 0);
        ui.SetActive(false);
    }


    public void ComprarOCancelar(int a)
    {
        comprarOCancelar = a;
    }
}
