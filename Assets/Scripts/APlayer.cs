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
    private Casilla[] tablero;
    private int miCasilla = 0;
    [Header("XP stuff")]
    // valores para empezar con 500 de xp
    public int xp = 500;
    public int level = 1;
    public int xpToLevelUp = 100;
    public int bote = 0; 

    //casillas especiales
    public bool estaEnCarcel = false;
    public GameObject cartaSuerte;

    private void Awake()
    {
        tablero = GameObject.FindWithTag("Tablero").GetComponent<Tablero>().GetTablero();
        if (tablero != null) transform.position = tablero[miCasilla].transform. position;//posicion inicial
        tablero[0].AddJugador(gameObject);
        CheckLevel();
    }
    private void CheckLevel()
    {
        // bajada de nivel
        while (xp < 0)
        {
            if (level <= 1)
            {
                // no se puede bajar más, se descarta la xp negativa
                Debug.Log("Jugador bajo del nivel 1, deberia perder partida");
                level = 1;
                xp = 0;
                xpToLevelUp = 100;//xp del nivel 1
                return;
            }

            level--;
            xpToLevelUp = Mathf.Max(100, xpToLevelUp / 2);//100 es la xp del nivel 1
            xp += xpToLevelUp; // xp es negativo, así que sumar
        }

        // subida de nivel
        while (xp >= xpToLevelUp)
        {
            level++;
            xp -= xpToLevelUp;
            xpToLevelUp *= 2; // cada vez cuesta más subir de nivel
        }
    }

    public IEnumerator SumarXP(int amount, float duration = 1f)
    {
        Debug.Log("Se sumará " + amount + " xp");

        int applied = 0;//xp sumada
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            int target = Mathf.RoundToInt(amount * t); // cuánto debería llevar sumado ya
            int step = target - applied;
            if (step != 0)
            {
                xp += step;
                applied = target;
                CheckLevel();
            }
            yield return null; // esperar al siguiente frame
        }
        //suma el resto
        if (applied != amount)
        {
            xp += amount - applied;
            CheckLevel();
        }
        //pausa final
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator AplicarEfectosDeCasilla()
    {
        if (tablero[miCasilla].tipo == TipoCasilla.ComparOvender)
        {
            yield return StartCoroutine(SumarXP(-tablero[miCasilla].coste));// compra obligada por ahora
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.Inicio)
        {
            Debug.Log(playerName + " ha pasado por la casilla de inicio y ha ganado 100 de XP");
            yield return StartCoroutine(SumarXP(100));// le da 100 de xp por pasar por inicio
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.CartaSuerte)
        {
            RobarCartaSuerte();
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.VeAcarcel)
        {
            IrCarcel();
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.Carcel)
        {
            // no hace nada, solo esta de paso
            Debug.Log(playerName + " visita la cárcel");
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.Bote)
        {
            GanarBote(); 
        }

    }

    public void GanarBote()
    {
        SumarXP(bote);
        bote = 0; 
        Debug.Log(playerName + " ha ganado el bote y ahora tiene " + xp + " de XP");
    }

    public void IrCarcel()
    {
        tablero[miCasilla].RemoveJugador(gameObject);
        miCasilla = System.Array.FindIndex(tablero, c => c.tipo == TipoCasilla.Carcel);//busca la casilla de la carcel
        tablero[miCasilla].AddJugador(gameObject);

        estaEnCarcel = true;
        Debug.Log(playerName + " ha ido a la cárcel");
    }

    public IEnumerator Mover(int n)//movimiento de las casillas
    {
        if (!estaEnCarcel)
        {
            tablero[miCasilla].RemoveJugador(gameObject);
            for (int i = 0; i < n; i++)
            {
                miCasilla++;
                miCasilla %= tablero.Length;
                transform.position = tablero[miCasilla].transform.position;
                yield return new WaitForSeconds(1f / n);//1 segundo en mover todas las n posiciones
            }
            tablero[miCasilla].AddJugador(gameObject);
            yield return StartCoroutine(AplicarEfectosDeCasilla());
        }
    }   

    public void OnFinishTurn()
    {
        miturno = false;
        //transform.localScale = Vector3.one * prevScale;// recupera tamaño
    }

    public void OnStartTurn()
    {
        miturno = true;
        transform.localScale *= 1.3f;// 30% mas grande
    }

    public void RobarCartaSuerte()
    {
        StartCoroutine(MostrarCartaTemporalmente());
        Debug.Log(playerName + " ha robado una carta de suerte");
    }

    private IEnumerator MostrarCartaTemporalmente()
    {
        cartaSuerte.SetActive(true);     
        yield return new WaitForSeconds(2f);// espera 2s para que vea la carta
        cartaSuerte.SetActive(false);
    }

    public void SalirCarcel()
    {
        estaEnCarcel = false;
        Debug.Log(playerName + " ha salido de la cárcel");
    }

    public IEnumerator TirarDado()
    {
        if (!miturno) numeroDado = 0;// por si a caso
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));// espera a que le pulse
        yield return new WaitForSeconds(0.2f);// tiempo de animacion
        numeroDado = miDado.LanzarDadoNormal();//lanza el dado
        if (numeroDado == 6 && estaEnCarcel)
        {
            SalirCarcel();
        }
    }
}
