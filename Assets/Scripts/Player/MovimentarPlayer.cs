using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour
{
    public FlipCorpoPlayer flipCorpo; //Variavel para poder fazer o flip da imagem do player
    public AnimacaoPlayer animacaoPlayer; //variavel com os codigos da animação do player
    public LimitePlayer limiteDireita;
    public LimitePlayer limiteEsquerda;
    public LimitePlayer limiteCabeca;
    public LimitePlayer limitePe;
    public Rigidbody2D rigidbody2d;


    public float velocidade; //velocidade do player

    public float forcaPuloY; //forca do pulo do player
    private float forcaPuloX; //força do pulo no eixo X
    private bool estaPulando; //variavel para saber se o player esta pulando
    private bool puloDuplo; //permite o player efetuar o pulo duplo
    private bool pularDaParede; //permitir pular da párede
    

    private Coroutine coroutinePulo; //contador para limitar o pulo do player

    // Start is called before the first frame update
    void Start()
    {
        //habilitar o pulo da parede ao iniciar o game
        pularDaParede = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //verificar se o jogo acabou
        if (CanvasGameMng.Instance.fimDeJogo == true) return;
        
        
        Movimentar();
        Pular();
        PularDaParede();
    }

    /// <summary>
    /// Efetua o pulo do player
    /// </summary>
    private void Pular()
    {
        //Obter a entrada do jogador para fazer o pulo
        if (Input.GetButtonDown("Jump"))
        {
            //Verificar se o pulo está habilitado e se o player esta no chão
            if (limitePe.estaNoLimite == true && estaPulando == false)
            {
                //ativa a animação de pulando
                animacaoPlayer.PlayPulando();
                
                //habilitar esta pulando
                estaPulando = true;

                //habilita o pulo duplo
                puloDuplo = true;

                //ativar o tempo do pulo
                AtivarTempoPulo();
            }
            else
            {
                //verificar se pode fazer o pulo duplo
                if (puloDuplo == true)
                {
                    //ativa a animação de pulo duplo
                    animacaoPlayer.PlayPuloDuplo();
                    
                    //habilita novamente o pulo duplo
                    estaPulando = true;

                    //desabilita o pulo duplo
                    puloDuplo = false;

                    //ativar o tempo do pulo
                    AtivarTempoPulo();
                }
            }
        }

        //efetuar o pulo
        EfetuarPulo();
    }

    /// <summary>
    /// Vai ativar o contador de tempo para o pulo
    /// </summary>
    private void AtivarTempoPulo()
    {
        // Verificar se ja existe um contador de tempo para o pulo
        if (coroutinePulo != null)
        {
            StopCoroutine(coroutinePulo); //vai parar o contador de tempo do pulo anterior
        }

        //criar um novo contador
        coroutinePulo = StartCoroutine(TempoPulo());
    }

    /// <summary>
    /// Lógica de movimentação do personagem
    /// </summary>
    private void Movimentar()
    {
        //Obter uma entrada do usuário para fazer a movimentação
        float eixoX = Input.GetAxis("Horizontal");

        //Verificar se chegou nos limites da esquerda ou da direita
        if (eixoX > 0 && limiteDireita.estaNoLimite == true)
        {
            eixoX = 0;
        }
        else if (eixoX < 0 && limiteEsquerda.estaNoLimite == true)
        {
            eixoX = 0;
        }

        //olhar par o lado que o jogador está movendo
        if (eixoX > 0)
        {
            flipCorpo.OlharDireita();
        }
        else if (eixoX < 0)
        {
            flipCorpo.OlharEsquerda();
        }
        
        //verificar se esta no chão para poder ativar as animações
        if (limitePe.estaNoLimite == true)
        {
            //verificar se player esta correndo
            if (eixoX !=0)
            {
                //ativa animação de correndo do player
                animacaoPlayer.PlayCorrendo();
            }
            else
            {
                //ativa animação de parado do player
                animacaoPlayer.PlayParado();
            }
        }
        else
        {
            //ativa animação de caindo do player
            animacaoPlayer.PlayCaindo();
        }

        //Definir a direção da movimentação
        Vector3 direcaoMovimento = new Vector3(eixoX, 0, 0);

        //Movimentar o personagem no sentido da direção
        transform.position += direcaoMovimento * velocidade * Time.deltaTime;
        
        // Se o player está caindo (não está no chão e não pulou ainda)
        if (!limitePe.estaNoLimite && estaPulando == false)
        {
            // habilita o pulo duplo
            puloDuplo = true;
        }

    }

    private IEnumerator TempoPulo()
    {
        //permitir o.3 segundo para o pulo
        yield return new WaitForSeconds(0.3f);

        //desabilitar a variavel que permite o player subir
        estaPulando = false;
        //fazer o player voltar a cair
        rigidbody2d.gravityScale = 4;
        
        //zerar a força no eixo x
        forcaPuloX = 0;
    }

    /// <summary>
    /// Metodo para poder fazer o jogador simular o pulo
    /// </summary>
    private void EfetuarPulo()
    {
        //Verificar se o player pode subir
        if (estaPulando == true)
        {
            //verificar se a cabeça do player esta livre para pular
            if (limiteCabeca.estaNoLimite == false)
            {

                //zerar as forças nos eixos do rigidbody
                ResetarFisicaDeMovimentacao();

                //alterar a propriedade do rigidbody para fazer o player subir
                rigidbody2d.gravityScale = 0;

                //direcionar o pulo do player
                Vector3 direcaoPulo = new Vector3(forcaPuloX, forcaPuloY, 0);

                //mover o player para cima, simbolizando o pulo
                transform.position += direcaoPulo * velocidade * Time.deltaTime;
            }
            else
            {
                //fazer o player voltar a cair
                rigidbody2d.gravityScale = 4;
            }
        }
    }

    /// <summary>
    /// faz o jogador pular atravez da parede
    /// </summary>
    private void PularDaParede()
    {
        // verificar se esta no chão para habilitar o pulo da parede novamente
        if (limitePe.estaNoLimite == true)
        {
            pularDaParede = true;
        }

        //verificar se o pular da parede esta permitido
        if (pularDaParede == false)
        {
            return;
        }

        /// Verificar se o player não está no chão, a cabeça não está no limite, e está na parede
        if (limitePe.estaNoLimite == false && limiteCabeca.estaNoLimite == false &&
            (limiteEsquerda.estaNoLimite == true || limiteDireita.estaNoLimite == true))
        {
            //ativa a animmação de deslizar na parede
            animacaoPlayer.PlayDeslizarParede();
            
            // Obter entrada do usuário para pular na parede
            if (Input.GetButtonDown("Jump"))
            {
                // Aplicar força em X na direção contrária à parede em que o jogador está encostado
                if (limiteDireita.estaNoLimite == true)
                {
                    forcaPuloX = forcaPuloY * -1; // Direção oposta à parede direita
                }
                else if (limiteEsquerda.estaNoLimite == true)
                {
                    forcaPuloX = forcaPuloY; // Direção oposta à parede esquerda
                }
                else
                {
                    forcaPuloX = 0; // Se não está encostado em nenhuma parede
                }
                
                //ativar animação de pulo
                animacaoPlayer.PlayPulando();

                // Habilitar o pulo
                estaPulando = true;

                // Habilitar o pulo duplo
                puloDuplo = true;

                // Desabilitar o pulo da parede (se estiver ativado)
                pularDaParede = false;

                // Ativar o contador de tempo do pulo
                AtivarTempoPulo();
            }
        }
    }

    /// <summary>
    /// arremesse o player para direção aleatoria
    /// </summary>
    public void ArremessarPlayer()
    {
        //sortear um numero entre 0 e 1 para poder definir a direção do arremesso
        int valorSorteado = new System.Random().Next(0, 2);

        //definir a direção em X a ser arremessado
        int direcaoX = valorSorteado == 0 ? -1000 : 1000;
        
        //aplicar força ao player
        rigidbody2d.AddForce(new Vector2(direcaoX, 500));
    }
    /// <summary>
    /// reseta as forças do rigidbody do player
    /// </summary>
    public void ResetarFisicaDeMovimentacao()
    {
        //zerar as forças nos eixos do rigidbody
        rigidbody2d.velocity = Vector3.zero;
    }
    
    /// <summary>
    /// metodo para remover gravidade
    /// </summary>
    public void RemoverGravidade()
    {
        //colocar o rigidbody do player como static
        rigidbody2d.bodyType = RigidbodyType2D.Static;
    }
    
    /// <summary>
    /// metodo para tirar as funções de movimentação do player
    /// </summary>
    public void CongelarPlayer()
    {
        //ResetarFisicaDeMovimentacao();
        ResetarFisicaDeMovimentacao();
        
        //remover a gravidade
        RemoverGravidade();
        
        //ativar a animação de parado do player
        animacaoPlayer.PlayParado();
    }
}