using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    public GameObject panelPausa;
   

    public ManagerTurnos manager;

    public void Update()
    {
        //si pulsa ESC se pausa el juego
        if (panelPausa != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (manager.parado)
            {
                cerrarPanel(); 
            }
            else
            {
                panelPausa.SetActive(true);
                manager.parado = true;
                Time.timeScale = 0f;
            }
        }
    }

    public void cerrarPanel()
    {
        panelPausa.SetActive(false);
        manager.parado = false;
        Time.timeScale = 1f;
    }

    public void cambiarEscena(int i)
    {
        if(manager != null) cerrarPanel();
        SceneManager.LoadScene(i); 
    }

}
