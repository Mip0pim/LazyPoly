using System.Collections.Generic;
using UnityEngine;

public enum TipoCasilla
{
    ComparOvender,
    Carcel, 
    Bote, 
    CartaSuerte, 
    VeAcarcel,
    Inicio
}

public class Casilla : MonoBehaviour
{
    private List<GameObject> ocupacion = new List<GameObject>(); // contiene el nombre de los jugadores que hay en esa casilla
    private const float prevScale = 6.0f;// esta harcodeado, habra que cambiarlo
    public TipoCasilla tipo = TipoCasilla.ComparOvender;//por defecto
    public int coste;//solo si es para comprar
    public int valorVenta;

    public List<GameObject> GetOcupacion()
    {
        return ocupacion;
    }

    public void AddJugador(GameObject jug)
    {
        ocupacion.Add(jug);
        Recolocar(); 
    }

    public void RemoveJugador(GameObject jug)
    {
        ocupacion.Remove(jug);
        jug.transform.localScale = Vector3.one * prevScale; // restablece el tamaño del jugador al salir de la casilla
        Recolocar();
    }

    // Recoloca los jugadores en la casilla dependiendo de cuantos hay
    public void Recolocar()
    {
        int nJug = ocupacion.Count; // numero de jugadores que hay actualmente en la casilla
        switch (nJug)
        {
            case 0:
                break;  // no hay que colocar nada

            case 1:
                ocupacion[0].transform.localPosition = transform.position; // coloca al jugador en el centro de la casilla
                ocupacion[0].transform.localScale = Vector3.one * prevScale; 
                break;

            case 2:
                ocupacion[0].transform.localPosition = new Vector3(transform.position.x + (-0.4f), transform.position.y , transform.position.z); // coloca al primer jugador a la izquierda
                ocupacion[1].transform.localPosition = new Vector3(transform.position.x + (0.4f), transform.position.y, transform.position.z); // coloca al segundo jugador a la derecha
                ocupacion[0].transform.localScale = Vector3.one * 0.8f * prevScale; // reduce el tamaño de los jugadores para que quepan
                ocupacion[1].transform.localScale = Vector3.one * 0.8f * prevScale;
                break;

            case 3:
                ocupacion[0].transform.localPosition = new Vector3(transform.position.x + (-0.3f), transform.position.y + 0.3f, transform.position.z); // coloca al primer jugador arriba a la izquierda
                ocupacion[1].transform.localPosition = new Vector3(transform.position.x + (0.3f), transform.position.y + 0.3f, transform.position.z); // coloca al segundo jugador arriba a la derecha
                ocupacion[2].transform.localPosition = new Vector3(transform.position.x , transform.position.y + (-0.3f), transform.position.z); // coloca al tercer jugador abajo en el centro
                ocupacion[0].transform.localScale = Vector3.one * 0.6f * prevScale; // reduce el tamaño de los jugadores para que quepan
                ocupacion[1].transform.localScale = Vector3.one * 0.6f * prevScale;
                ocupacion[2].transform.localScale = Vector3.one * 0.6f * prevScale;
                break;

            case 4:
                ocupacion[0].transform.localPosition = new Vector3(transform.position.x + (-0.3f), transform.position.y + 0.3f, transform.position.z); // coloca al primer jugador arriba a la izquierda
                ocupacion[1].transform.localPosition = new Vector3(transform.position.x + (0.3f), transform.position.y + 0.3f, transform.position.z); // coloca al segundo jugador arriba a la derecha
                ocupacion[2].transform.localPosition = new Vector3(transform.position.x + (-0.3f), transform.position.y + (-0.3f), transform.position.z); // coloca al tercer jugador abajo a la izquierda
                ocupacion[3].transform.localPosition = new Vector3(transform.position.x + (0.3f), transform.position.y + (-0.3f), transform.position.z); // coloca al cuarto jugador abajo a la derecha
                ocupacion[0].transform.localScale = Vector3.one * 0.4f * prevScale; // reduce el tamaño de los jugadores para que quepan
                ocupacion[1].transform.localScale = Vector3.one * 0.4f * prevScale;
                ocupacion[2].transform.localScale = Vector3.one * 0.4f * prevScale;
                ocupacion[3].transform.localScale = Vector3.one * 0.4f * prevScale;
                break;
        }
    }
}
