using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTurnos : MonoBehaviour
{
    [Header("Players")]
    public APlayer[] players;
    [SerializeField] private int turno;
    [Header("UI")]
    public TextMeshProUGUI nombreJugador;
    public TextMeshProUGUI nivel;
    public TextMeshProUGUI infoXP;
    public Image barraXP;


    private APlayer player;

    private Coroutine turnoCoroutine;

    private void Awake()
    {
        turno = 0;// por defecto creo que es 0 pero para estar seguros
        player = players[0];
    }


    private void Update()
    {
        if (players.Length < 1) return;// no hay players
        if(turnoCoroutine == null) turnoCoroutine = StartCoroutine(TurnoJugador(turno));
        
        
    }


    IEnumerator TurnoJugador(int id)
    {
        //setup
        player = players[id];
        UpdateUIPlayer();
        
        player.OnStartTurn();//comienza el turno
        yield return StartCoroutine(player.TirarDado());//tira el dado
        int dado = player.numeroDado;
        yield return new WaitForSeconds(0.2f);//espera un poco
        yield return StartCoroutine(player.Mover(dado));//muevete
        player.OnFinishTurn();//ternima el turno

        // siguiente jugador
        turno++;
        turno%=players.Length;
        turnoCoroutine = null;
    }

    private void UpdateUIPlayer()
    {
        if (nombreJugador != null) nombreJugador.text = player.playerName;

    }

    private void OnGUI()
    {
        if (nivel != null) nivel.text = "Nivel: " + player.level;
        if (infoXP != null) infoXP.text = "XP: " + player.xp + " / " + player.xpToLevelUp;
        float ratio = (float)player.xp / player.xpToLevelUp;
        if (ratio == 0) ratio = 0.01f;//poner un 1% si esta a 0 para mostrar algo
        if (barraXP != null) barraXP.fillAmount = ratio;
    }
}
