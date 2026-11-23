using System.Collections;
using TMPro;
using UnityEngine;

public class APlayer : MonoBehaviour, IPlayer
{
    [Header("Player Name")]
    public string playerName = "Default Player";
    public int playerId = 0;
    [Header("Player Dice")]
    public Dado miDado;
    private bool miturno = false;
    private const float prevScale = 0.08f;// esta harcodeado, habra que cambiarlo
    public int numeroDado;
    private Casilla[] tablero;
    private int miCasilla = 0;
    [Header("XP stuff")]
    // valores para empezar con 500 de xp
    public int xp = 500;
    public int level = 1;
    public int xpToLevelUp = 100;
    public int bote = 0; 
    private float mult = 1f;
    //casillas especiales
    public bool estaEnCarcel = false;
    public GameObject cartaSuerte;

    [Header("Interfaz")]
    public TextMeshProUGUI nombreUI;
    public TextMeshProUGUI nivelUI;
    public TextMeshProUGUI xpUI; 


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
        Debug.Log("Se sumará " + amount*mult + " xp a " + playerName);

        int applied = 0;//xp sumada
        float elapsed = 0f;
        amount = (int)(amount * mult);
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
            Casilla casilla = tablero[miCasilla];
            if (casilla.nivelParaCompar <= level)
            {
                if (casilla.jugadorQuePertenece == -1)
                {
                    tablero[miCasilla].jugadorQuePertenece = playerId;
                    CompraVentaUI cvUI = GameObject.FindWithTag("CompraVentaUI").GetComponent<CompraVentaUI>();
                    yield return StartCoroutine(cvUI.ActivarCompra(casilla));
                    if (cvUI.comprarOCancelar == 1)
                    {
                        yield return StartCoroutine(SumarXP(-casilla.coste));// compra 
                    }
                    
                }
                else
                {
                    ManagerTurnos mt = GameObject.FindWithTag("ManagerTurnos").GetComponent<ManagerTurnos>();
                    mt.players[casilla.jugadorQuePertenece].xp += casilla.valorVenta;
                    yield return StartCoroutine(SumarXP(-casilla.valorVenta));
                }
            }
            else
            {
                Debug.Log("Jugador no tiene suficiente nivel para comprar");
            }
                
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.Inicio)
        {
            Debug.Log(playerName + " ha pasado por la casilla de inicio y ha ganado 1000 de XP");
            yield return StartCoroutine(SumarXP(1000));// le da 100 de xp por pasar por inicio
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.CartaSuerte)
        {
            yield return StartCoroutine(RobarCartaSuerte());
        }
        else if (tablero[miCasilla].tipo == TipoCasilla.VeAcarcel)
        {
            yield return StartCoroutine(IrCarcel());
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
        StartCoroutine(SumarXP(bote));
        bote = 0; 
        Debug.Log(playerName + " ha ganado el bote y ahora tiene " + xp + " de XP");
    }

    public IEnumerator IrCarcel()
    {
        int casillaCarcel = System.Array.FindIndex(tablero, c => c.tipo == TipoCasilla.Carcel);

        if (casillaCarcel == -1)
        {
            Debug.LogError("No se encontró ninguna casilla de Cárcel en el tablero.");
            yield break;
        }

        // calcula los pasos siempre hacia adelante, dando la vuelta al tablero si es necesario
        int pasos = (casillaCarcel - miCasilla + tablero.Length) % tablero.Length;
        yield return StartCoroutine(Mover(pasos));

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
        mult = 1f;//reinicia mult
        miturno = false;
        //transform.localScale = Vector3.one * prevScale;// recupera tamaño
    }

    public void OnStartTurn()
    {
        miturno = true;
        miDado.Idle();
        transform.localScale *= 1.3f;// 30% mas grande
    }

    public IEnumerator RobarCartaSuerte()
    {
        Carta cartaSuerte = GameObject.FindWithTag("CartaSuerteBD")?.GetComponent<CartasSuerte>().GetNextCard();
        if (cartaSuerte == null)
        {
            Debug.Log("Carta de la suerte fue nulo");
            yield break;//como un return en corrutinas
        }
        StartCoroutine(MostrarCartaTemporalmente());
        Debug.Log(playerName + " ha robado una carta de suerte");
        // hacer una accion dependiendo de la carta
        if (cartaSuerte.tipoDeCarta == TipoCartaSuerte.VeCarcel)
        {
            Debug.Log("Carta suerte: ve carcel");
            yield return StartCoroutine(IrCarcel());
        }
        if (cartaSuerte.tipoDeCarta == TipoCartaSuerte.CambiaMult)
        {
            Debug.Log("Carta suerte: mult: " + cartaSuerte.valor);
            mult = cartaSuerte.valor;
        }
        if (cartaSuerte.tipoDeCarta == TipoCartaSuerte.GanaOPierdeXP)
        {
            Debug.Log("Carta suerte: gana xp: " + cartaSuerte.valor);
            yield return StartCoroutine(SumarXP(cartaSuerte.valor));
        }
        yield return null;
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
        yield return StartCoroutine(miDado.LanzarDadoNormal());//lanza el dado
        numeroDado = miDado.numero;
        if (numeroDado == 6 && estaEnCarcel)
        {
            SalirCarcel();
        }
    }

    private void OnGUI()
    {
        if (nombreUI != null) nombreUI.text = playerName;
        if (nivelUI != null) nivelUI.text = "Nivel: " + level;
        if (xpUI != null) xpUI.text = "XP: " + xp + " / " + xpToLevelUp;
    }
}
