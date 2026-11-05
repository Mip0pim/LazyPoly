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
        //quito el no implemntado para que no de error
        if (tablero[miCasilla].tipo == TipoCasilla.ComparOvender)
        {
            yield return StartCoroutine(SumarXP(-tablero[miCasilla].coste));// compra obligada por ahora
        }
        else
        {
            ///mas tipos de casillas
        }
        
    }

    public void IrCarcel()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator Mover(int n)//movimiento de las casillas
    {
        tablero[miCasilla].RemoveJugador(gameObject); 
        for (int i = 0;i<n;i++)
        {
            miCasilla++;
            miCasilla%=tablero.Length;
            transform.position = tablero[miCasilla].transform.position;
            yield return new WaitForSeconds(1f/n);//1 segundo en mover todas las n posiciones
        }
        tablero[miCasilla].AddJugador(gameObject);
        yield return StartCoroutine(AplicarEfectosDeCasilla());
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
