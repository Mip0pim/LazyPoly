using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectJugador : MonoBehaviour
{
    public Button jug11;
    public Button jug21;
    public Button jug31;
    public Button jug41;

    public Button jug12;
    public Button jug22;
    public Button jug32;
    public Button jug42;

    public int select = -1; 

    public void selectJug(int n)
    {
        switch (n)
        {
            case 5:
                jug11.gameObject.SetActive(false);
                jug21.gameObject.SetActive(true);
                jug31.gameObject.SetActive(true);
                jug41.gameObject.SetActive(true);
                jug12.gameObject.SetActive(true);
                jug22.gameObject.SetActive(false);
                jug32.gameObject.SetActive(false);
                jug42.gameObject.SetActive(false);
                select = 5; 
                break;
            case 6:
                jug11.gameObject.SetActive(true);
                jug21.gameObject.SetActive(false);
                jug31.gameObject.SetActive(true);
                jug41.gameObject.SetActive(true);
                jug12.gameObject.SetActive(false);
                jug22.gameObject.SetActive(true);
                jug32.gameObject.SetActive(false);
                jug42.gameObject.SetActive(false);
                select = 6;
                break;
            case 7:
                jug11.gameObject.SetActive(true);
                jug21.gameObject.SetActive(true);
                jug31.gameObject.SetActive(false);
                jug41.gameObject.SetActive(true);
                jug12.gameObject.SetActive(false);
                jug22.gameObject.SetActive(false);
                jug32.gameObject.SetActive(true);
                jug42.gameObject.SetActive(false);
                select = 7;
                break;
            case 8:
                jug11.gameObject.SetActive(true);
                jug21.gameObject.SetActive(true);
                jug31.gameObject.SetActive(true);
                jug41.gameObject.SetActive(false);
                jug12.gameObject.SetActive(false);
                jug22.gameObject.SetActive(false);
                jug32.gameObject.SetActive(false);
                jug42.gameObject.SetActive(true);
                select = 8;
                break;
            case 1:
                jug11.gameObject.SetActive(true);
                jug21.gameObject.SetActive(false);
                jug31.gameObject.SetActive(false);
                jug41.gameObject.SetActive(false);
                jug12.gameObject.SetActive(false);
                jug22.gameObject.SetActive(true);
                jug32.gameObject.SetActive(true);
                jug42.gameObject.SetActive(true);
                select = 1;
                break;
            case 2:
                jug11.gameObject.SetActive(false);
                jug21.gameObject.SetActive(true);
                jug31.gameObject.SetActive(false);
                jug41.gameObject.SetActive(false);
                jug12.gameObject.SetActive(true);
                jug22.gameObject.SetActive(false);
                jug32.gameObject.SetActive(true);
                jug42.gameObject.SetActive(true);
                select = 2;
                break;
            case 3:
                jug11.gameObject.SetActive(false);
                jug21.gameObject.SetActive(false);
                jug31.gameObject.SetActive(true);
                jug41.gameObject.SetActive(false);
                jug12.gameObject.SetActive(true);
                jug22.gameObject.SetActive(true);
                jug32.gameObject.SetActive(false);
                jug42.gameObject.SetActive(true);
                select = 3;
                break;
            case 4:
                jug11.gameObject.SetActive(false);
                jug21.gameObject.SetActive(false);
                jug31.gameObject.SetActive(false);
                jug41.gameObject.SetActive(true);
                jug12.gameObject.SetActive(true);
                jug22.gameObject.SetActive(true);
                jug32.gameObject.SetActive(true);
                jug42.gameObject.SetActive(false);
                select = 4;
                break;
        }
    }
}
