using UnityEngine;

public class Mola  : MonoBehaviour
{
    private Animator oAnimator;
    public float forcaDaMola;

    void Awake()
    {
        oAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            oAnimator.Play("animacao-da-mola-subindo");
            // Ajuste a força do impulso conforme necessário
            
                other.gameObject.GetComponent<MovimentoDoJogador>().ImpulsionarJogador(forcaDaMola);
        }
    }
}
