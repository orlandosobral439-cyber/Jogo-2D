using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoDoJogador : MonoBehaviour
{
    [Header("Referências")]
    private Rigidbody2D oRigidBody2D;
     private Animator oAnimator;

    [Header("Movimento Horizontal do Jogador")]
    public float velocidadeDoJogador = 8f;
    public bool indoParaDireita;
   
    public Transform verificadorDeChao;
    public float tamanhoRaioDeVerificacao;
    public LayerMask layerDoChao;
    public float alturaDoPulo;
    private bool estaNoChao;
    public bool jogadorEstaVivo;

    [Header("Wall Jump")]
    public Transform verificadorDeParede;
    public bool estaPulandonaParede;
    public bool estaNaParede;
    public float wallJumpForceX;
    public float wallJumpForceY;
    
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
        if(jogadorEstaVivo  == true)
        {
            MovimentarJogador();
            Pular();
            WallJump();    
        }            
    }

    private void MovimentarJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        oRigidBody2D.linearVelocity = new Vector2(movimentoHorizontal * velocidadeDoJogador,oRigidBody2D.linearVelocity.y);

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

        
        else if (movimentoHorizontal == 0)
        {
             oAnimator.Play("jogador-idle");
        }
    
    }

    private void Pular()
    {
        // Verifica se o personagem está tocando o chão
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoRaioDeVerificacao, layerDoChao);

        // Se apertar espaço e estiver no chão -> pula normal
        if (Keyboard.current.spaceKey.isPressed && estaNoChao == true)
        {
            oRigidBody2D.AddForce(new Vector2(0f, alturaDoPulo), ForceMode2D.Impulse);
            Debug.Log("Pulou");
        }

        // Se não estiver no chão -> animação de pulo
        if (estaNoChao == false)
        {
            oAnimator.Play("jogador-pulando");
        }
        
    }
    private void WallJump()
    {
        estaNaParede = Physics2D.OverlapCircle(verificadorDeParede.position, tamanhoRaioDeVerificacao, layerDoChao);

        if (Keyboard.current.spaceKey.isPressed && estaNaParede == true && estaNoChao == false)
        {
            estaPulandonaParede = true;
        }
        if (estaPulandonaParede == true)
        {
            if (indoParaDireita == true)
         {
                oRigidBody2D.linearVelocity = new Vector2(-wallJumpForceX, wallJumpForceY);
            }
            else
            {
                oRigidBody2D.linearVelocity = new Vector2(wallJumpForceX, wallJumpForceY);
            }
            Invoke(nameof(DeixarEstarPulandoNaParedeComoFalso), 0.1f);
        }
    }
    
       private void DeixarEstarPulandoNaParedeComoFalso()
       {
        estaPulandonaParede = false;
       }
    public void ImpulsionarJogador(float forcaDoImpulso)
    {
    oRigidBody2D.linearVelocity = new Vector2(oRigidBody2D.linearVelocity.x, 0f);
     oRigidBody2D.AddForce(new Vector2(0f, forcaDoImpulso), ForceMode2D.Impulse);
}
}