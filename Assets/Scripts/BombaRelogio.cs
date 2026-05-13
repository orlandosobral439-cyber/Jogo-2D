using UnityEngine;
using System.Collections;

public class BombaRelogio : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoParaAtivar = 2f;
    public GameObject efeitoDeExplosao;

    private bool ativado = false;
    private bool jogadorEmCima = false;
    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !ativado)
        {
            jogadorEmCima = true;
            playerCollider = other;
            StartCoroutine(SequenciaCompleta());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorEmCima = false;
        }
    }

    IEnumerator SequenciaCompleta()
    {
        ativado = true;

        // Espera 2 segundos
        yield return new WaitForSeconds(tempoParaAtivar);

        // Se o jogador ainda estiver em cima → toma dano
        if (jogadorEmCima && playerCollider != null)
        {
            VidaDoJogador vida = playerCollider.GetComponent<VidaDoJogador>();
            if (vida != null)
            {
                vida.MachucarJogador();
            }
        }

        // Explosão
        if (efeitoDeExplosao != null)
        {
            Instantiate(efeitoDeExplosao, transform.position, transform.rotation);
        }

        // Destroi a barra
        Destroy(gameObject);
    }
}

