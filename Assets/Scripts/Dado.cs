
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite[] caras;
    [SerializeField] private Animator anim;

    [Header("Sonidos")]
    [SerializeField] private AudioSource sonidoLanzamiento;
    [SerializeField] private AudioSource sonidoRevolver;

    public int numero;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();

        if (anim == null)
            anim = GetComponent<Animator>();

        numero = 0;
    }

    // Comenzar a revolver automáticamente
    public void IniciarRevolver()
    {
        anim.Play("idle");

        if (!sonidoRevolver.isPlaying)
            sonidoRevolver.Play();
    }

    // Detener el sonido de revolver
    public void DetenerRevolver()
    {
        sonidoRevolver.Stop();
    }

    // Sonido al pulsar ESPACIO
    public void SonidoAlLanzar()
    {
        sonidoLanzamiento.Stop();
        sonidoLanzamiento.time = 0f;
        sonidoLanzamiento.Play();
    }

    public void Idle()
    {
        anim.Play("idle");
    }

    // Mostrar el resultado del lanzamiento
    public IEnumerator LanzarDadoNormal()
    {
        numero = Random.Range(1, 7);

        anim.Play(numero.ToString());

        yield return new WaitForSeconds(2.5f);

        sp.sprite = caras[numero - 1];
    }

    public int LanzarDadoPersonalizado(int min, int max)
    {
        numero = Random.Range(min, max + 1);
        sp.sprite = caras[numero - 1];

        return numero;
    }
}
