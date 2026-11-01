using UnityEngine;

public class Tablero : MonoBehaviour
{
    [SerializeField] private Transform[] casillas;

    public Transform[] GetTablero()
    {
        return casillas;
    }
}
