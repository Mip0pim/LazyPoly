using TMPro;
using UnityEngine;

public class Dado : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite[] caras;
    private int numero;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        numero = 0;
    }
    private void Update()
    {
        //algn tipo de animacion idle
    }


    public int LanzarDadoNormal()
    {
        numero = Random.Range(1, 7);
        sp.sprite = caras[numero - 1];
        return numero;
    }
    public int LanzarDadoPersonalizado(int min, int max)
    {
        numero = Random.Range(min, max+1);
        sp.sprite = caras[numero - 1];
        return numero;
    }
}
