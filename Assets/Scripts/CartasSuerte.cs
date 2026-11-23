using System.Collections.Generic;
using UnityEngine;

public enum TipoCartaSuerte
{
    GanaOPierdeXP, 
    CambiaMult,
    VeCarcel
}
[System.Serializable]
public class Carta
{
    public string description = string.Empty;
    public TipoCartaSuerte tipoDeCarta = TipoCartaSuerte.GanaOPierdeXP;
    public int valor = 0;
}

public class CartasSuerte : MonoBehaviour
{
    [SerializeField]private List<Carta> cartas = new List<Carta>();
    private int index = 0;

    private void Awake()
    {
        //shuffle de cartas
        ShuffleCartas();
    }
    public Carta GetNextCard()
    {
        index++;
        index%= cartas.Count;
        return cartas[index];
    }

    private void ShuffleCartas()
    {
        for (int i = cartas.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (cartas[i], cartas[randomIndex]) = (cartas[randomIndex], cartas[i]);
        }
        index = 0; // reset
    }

}
