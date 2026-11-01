using System.Collections;
using TMPro;
using UnityEngine;

public class ManagerTurnos : MonoBehaviour
{
    [Header("Players")]
    public APlayer[] players;
    [SerializeField] private int turno;
    [Header("UI")]
    public TextMeshProUGUI nombreJugador;

    private Coroutine turnoCoroutine;

    private void Awake()
    {
        turno = 0;// por defecto creo que es 0 pero para estar seguros
        
    }


    private void Update()
    {
        if (players.Length < 1) return;// no hay players
        if(turnoCoroutine == null) turnoCoroutine = StartCoroutine(TurnoJugador(turno));
        
        
    }


    IEnumerator TurnoJugador(int id)
    {
        //setup
        APlayer player = players[id];
        if (nombreJugador != null) nombreJugador.text = player.playerName;
        
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


}
