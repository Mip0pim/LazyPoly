using UnityEngine;

public class Tablero : MonoBehaviour
{
    [SerializeField] private Casilla[] casillas;

    public Casilla[] GetTablero()
    {
        return casillas;
    }
}
