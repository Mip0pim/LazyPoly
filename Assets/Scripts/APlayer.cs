using System.Collections;
using UnityEngine;

public class APlayer : MonoBehaviour, IPlayer
{
    [Header("Player Name")]
    public string playerName = "Default Player";
    [Header("Player Dice")]
    public Dado miDado;
    private bool miturno = false;
    private const float prevScale = 6.0f;// esta harcodeado, habra que cambiarlo
    public int numeroDado;
    private Transform[] tablero;
    private int miCasilla = 0;

    private void Awake()
    {
        tablero = GameObject.FindWithTag("Tablero").GetComponent<Tablero>().GetTablero();
        if (tablero != null) transform.position = tablero[miCasilla].position;//posicion inicial
    }

    public IEnumerator AplicarEfectosDeCasilla()
    {
        //quito el no implemntado para que no de error
        yield return null;
    }

    public void IrCarcel()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator Mover(int n)//movimiento de las casillas
    {
        for (int i = 0;i<n;i++)
        {
            miCasilla++;
            miCasilla%=tablero.Length;
            transform.position = tablero[miCasilla].position;
            yield return new WaitForSeconds(1f/n);//1 segundo en mover todas las n posiciones
        }
        yield return StartCoroutine(AplicarEfectosDeCasilla());
    }

    public void OnFinishTurn()
    {
        miturno = false;
        transform.localScale = Vector3.one * prevScale;// recupera tamaño
    }

    public void OnStartTurn()
    {
        miturno = true;
        transform.localScale = Vector3.one * prevScale * 1.2f;// 20% mas grande
    }

    public void RobarCartaSuerte()
    {
        throw new System.NotImplementedException();
    }

    public void SalirCarcel()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator TirarDado()
    {
        if (!miturno) numeroDado = 0;// por si a caso
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));// espera a que le pulse
        yield return new WaitForSeconds(0.2f);// tiempo de animacion
        numeroDado = miDado.LanzarDadoNormal();//lanza el dado
    }
}
