using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Dado : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite[] caras;
    [SerializeField] private Animator anim;

    public int numero;
    private bool estaLanzando = false;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        if (anim == null) anim = GetComponent<Animator>();

        numero = 0;
    }
    private void Update()
    {

    }
    public void Idle()
    {
        anim.Play("idle");
   
    }


    public IEnumerator LanzarDadoNormal()
    {
        numero = Random.Range(1, 7);
        anim.Play((numero).ToString());
        yield return new WaitForSeconds(2.5f);//tiempo de animacion
        sp.sprite = caras[numero - 1];
    }
    public int LanzarDadoPersonalizado(int min, int max)
    {
        numero = Random.Range(min, max+1);
        sp.sprite = caras[numero - 1];
        return numero;
    }

}
