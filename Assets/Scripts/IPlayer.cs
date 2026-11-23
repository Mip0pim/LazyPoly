using System.Collections;

public interface IPlayer
{
    IEnumerator IrCarcel();
    void SalirCarcel();
    IEnumerator TirarDado();
    IEnumerator Mover(int n);
    IEnumerator AplicarEfectosDeCasilla();//comprar videojuegos y pagar a otros
    IEnumerator RobarCartaSuerte();
    void OnStartTurn();
    void OnFinishTurn();//hace algo al ternimar el turno?
}
