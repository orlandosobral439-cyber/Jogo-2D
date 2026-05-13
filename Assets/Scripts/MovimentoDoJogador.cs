using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoDoJogador : MonoBehaviour
{
    [Header("Referências")]
    private Rigidbody2D oRigidBody2D;
    private Animator oAnimator;

    [Header("Movimento Horizontal do Jogador ")]
    public float velocidadeDoJogador = 5f;
    public bool indoParaDireita;

    [Header("Pulo ")]
    public bool estaNoChao;
    public float alturaDoPulo;
    public float tamanhoDoRaioDeVerificacao;
    public Transform verificadorDeChao;
    public LayerMask layerDoChao;

    [Header("Wall Jump")]
    public bool estaNaParede;
    public bool estaPulandoNaParede;
    public float forcaXDoWallJump;
    public float forcaYDoWallJump;
    public Transform verificadorDeParede;

    [Header("Verificações")]
    public bool jogadorEstaVivo;

    // O método Awake é chamado quando o script é carregado, antes do início do jogo.
    void Awake()
    {
        oRigidBody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        jogadorEstaVivo = true;
    }
    void Update()
    {
        if (jogadorEstaVivo == true)
        {
            MovimentarJogador();
            Pular();
            WallJump();
        }
    }
/*
    private void MovimentarJogador()
    {
        */
        /*
            float horizontal = 0f;

            // Movimento do jogador...
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                horizontal = 1f;
                indoParaDireita = true;
                oAnimator.Play("jogador-andando");
            }
            else if (Keyboard.current.leftArrowKey.isPressed)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                horizontal = -1f;
                indoParaDireita = false;
                oAnimator.Play("jogador-andando");
            }
            else if (estaNoChao == true)
            {
                oAnimator.Play("jogador-idle");
            }

            oRigidBody2D.linearVelocity = new Vector2(horizontal * velocidadeDoJogador, oRigidBody2D.linearVelocity.y);
        */
/*
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal");

        oRigidBody2D.linearVelocity = new Vector2(movimentoHorizontal * velocidadeDoJogador, oRigidBody2D.linearVelocity.y);

        if (movimentoHorizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            indoParaDireita = true;
            oAnimator.Play("jogador-andando");
        }
        else if (movimentoHorizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            indoParaDireita = false;
            oAnimator.Play("jogador-andando");
        }
        else if (estaNoChao == true)
        {
            oAnimator.Play("jogador-idle");
        }
    }
    */
/*
    private void Pular()
    {
        // Verifica se o jogador está encostando no chão
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoRaioDeVerificacao, layerDoChao);

        // Faz o jogador Pular
        if (Keyboard.current.spaceKey.isPressed && estaNoChao == true)
        {
            oRigidBody2D.AddForce(new Vector2(0f, alturaDoPulo), ForceMode2D.Impulse);
            Debug.Log("Pulo");
        }
        if (estaNoChao == false)
        {
            oAnimator.Play("jogador-pulando");
        }
    }
*/
 private void MovimentarJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        oRigidBody2D.linearVelocity = new Vector2(movimentoHorizontal * velocidadeDoJogador, oRigidBody2D.linearVelocity.y);

        if (movimentoHorizontal > 0 && estaNoChao == true && estaNaParede == false)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            indoParaDireita = true;
            oAnimator.Play("jogador-andando");
        }
        else if (movimentoHorizontal < 0 && estaNoChao == true && estaNaParede == false)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            indoParaDireita = false;
            oAnimator.Play("jogador-andando");
        }

        else if (movimentoHorizontal == 0 && estaNoChao == true && estaNaParede == false)
        {
            oAnimator.Play("jogador-idle");
        }
    }

    private void Pular()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoRaioDeVerificacao, layerDoChao);

        if (Keyboard.current.spaceKey.isPressed && estaNoChao == true)
        {
            oRigidBody2D.AddForce(new Vector2(0f, alturaDoPulo), ForceMode2D.Impulse);
        }

        if (estaNoChao == false && estaNaParede == false)
        {
            oAnimator.Play("jogador-pulando");
        }
    }


    private void WallJump()
    {
        //Se o jogador está encostando na parede.
        //Verifica se está na parede
        estaNaParede = Physics2D.OverlapCircle(verificadorDeParede.position, tamanhoDoRaioDeVerificacao, layerDoChao);

        if (estaNaParede == true && estaNoChao == false)
        {
            oAnimator.Play("jogador-deslizando-na-parede");
        }
       if (estaNaParede == true && estaNoChao == true)
        {
            oAnimator.Play("jogador-idle");
        }
        if (Keyboard.current.spaceKey.isPressed && estaNaParede == true && estaNoChao == false)
        {
            estaPulandoNaParede = true;
        }

        if (estaPulandoNaParede == true)
        {
            if (indoParaDireita == true)
            {
                oRigidBody2D.linearVelocity = new Vector2(-forcaXDoWallJump, forcaYDoWallJump);
            }
            else
            {
                oRigidBody2D.linearVelocity = new Vector2(forcaXDoWallJump, forcaYDoWallJump);
            }
            // Ajuste o tempo conforme necessário para garantir que o jogador tenha tempo suficiente para realizar o wall jump.
            Invoke(nameof(DeixarEstarPulandoNaParedeComoFalso), 0.1f);
        }
    }

    private void DeixarEstarPulandoNaParedeComoFalso()
    {
        estaPulandoNaParede = false;
    }

    public void ImpulsionarJogador(float forcaDoImpulso)
    {
        //Reseta a velocidade vertical antes de aplicar a força para garantir um impulso consistente.
        oRigidBody2D.linearVelocity = new Vector2(oRigidBody2D.linearVelocity.x, 0f); 
        oRigidBody2D.AddForce(new Vector2(0f, forcaDoImpulso), ForceMode2D.Impulse);
    }
}