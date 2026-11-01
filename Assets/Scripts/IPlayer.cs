using System.Collections;

public interface IPlayer
{
    void IrCarcel();
    void SalirCarcel();
    IEnumerator TirarDado();
    IEnumerator Mover(int n);
    IEnumerator AplicarEfectosDeCasilla();//comprar videojuegos y pagar a otros
    void RobarCartaSuerte();
    void OnStartTurn();
    void OnFinishTurn();//hace algo al ternimar el turno?
}
