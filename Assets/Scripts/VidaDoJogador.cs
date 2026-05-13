using UnityEngine;
using System.Collections;

public class VidaDoJogador : MonoBehaviour
{
    [Header("Referências")]
    public GameObject efeitoDeExplosao;
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator;

    [Header("Valores")]
    public float tempoParaDestruirOJogador;

    void Awake()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
    }

    public void MachucarJogador()
    {
        FindFirstObjectByType<MovimentoDoJogador>().jogadorEstaVivo = false;

        oRigidbody2D.linearVelocity = Vector2.zero;
        oAnimator.Play("Jogador-Levando-Dano");

        StartCoroutine(DestruirJogador());
    }

    private IEnumerator DestruirJogador()
    {
        yield return new WaitForSeconds(tempoParaDestruirOJogador);
        FindFirstObjectByType<GameManager>().GameOver();
        Instantiate(efeitoDeExplosao, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}