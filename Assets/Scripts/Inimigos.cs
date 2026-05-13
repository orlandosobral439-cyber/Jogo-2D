using UnityEngine;

public class Inimigos  : MonoBehaviour
{
    [Header("Caminho do Inimigo")]
    public Transform[] pontosDoCaminho;
    public int pontoAtual;

    [Header("Movimento do Inimigo")]
    public float velocidadeDoInimigo;
    public float ultimaPosicaoX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pontoAtual = 0;
        transform.position = pontosDoCaminho[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        MoverInimigo();
        EspelharInimigo();
    }
    private void MoverInimigo()
{
    //Mover o inimigo entre os pontos do caminho
    transform.position = Vector2.MoveTowards(transform.position, pontosDoCaminho[pontoAtual].position, velocidadeDoInimigo * Time.deltaTime);

    //Verificar se o inimigo chegou ao ponto atual do caminho
    if(transform.position == pontosDoCaminho[pontoAtual].position)
    {
        //Avançar para o próximo ponto do caminho
        pontoAtual++;
        //Salvar a última posição X do inimigo para determinar a direção do movimento
        ultimaPosicaoX = transform.localPosition.x;

        //Verificar se o inimigo chegou ao último ponto do caminho e reiniciar o ciclo
        if(pontoAtual >= pontosDoCaminho.Length)
        {
            pontoAtual = 0;
        }
    }
}

   
    private void EspelharInimigo()
    {
        //Espelhar o inimigo com base na direção do movimento
        if(transform.localPosition.x < ultimaPosicaoX)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(transform.localPosition.x > ultimaPosicaoX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}