using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasGameMng : MonoBehaviour
{
    #region Singleton

    //criar uma variavel para instacia o objeto
    public static CanvasGameMng Instance;

    void Awake()
    {
        //criar a instancia estatica do objeto
        //verificar se ja existe uma instancia na cena
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        //caso contrario, destruir o objeto
        Destroy(gameObject);
    }

    #endregion
    public bool fimDeJogo;//Diz se o jogo acabou
    private PlayerControlador playerControlador; //codigo que controlam os demais codigos do player

    [Header("Vida do Jogador")]
    public Image imgVida; //imagem da vida
    public Sprite[] sptVida; //imagens da vida
    private int totalVidas; //quantidade de vidas que o player tem
    
    [Header("Controle do Painel Topo")]
    public TextMeshProUGUI txtTotalItensColetados;//texto que exibe o total de itens coletados
    private int totalItensColetados;//armazena o total de itens coletados
    public TextMeshProUGUI txtTempoDeJogo;//texto que exibe o tempo de jogo
    public float tempoDeJogo;//armazena o tempo de jogo
    public GameObject pnlTopo;//gameobject do painel do topo da tela
    
    [Header("Controle do Painel Level Completado")]
    public GameObject pnlLevelCompletado;//gameobject do painel da tela de level completado
    public TextMeshProUGUI txtTotalItensColetadosFinal;//texto com o total coletado do final do level
    public Image imgIconeMedalha; //imagem da medalha
    public Sprite[] sptsMedalhas; //sprites com as medalhas, 1 - bronze, 2 - prata, 3 - ouro
    
    private float qtdDeItensColetaveisNoLevel; //quantidade de itens coletaveis no level  
    private int idMedalha;//identificador da medalha que o jogador conseguiu no level
    
    // Start is called before the first frame update
    void Start()
    {
        tempoDeJogo += 1;
        //adicionar o total de vidas que o player tem ao iniciar o jogo
        totalVidas = sptVida.Length - 1;
        
        //pegar a referencia do controlador do player
        playerControlador = FindObjectOfType<PlayerControlador>();
        
        //zerar o total de itens coletados
        totalItensColetados = 0;
        
        //atualizar o texto da quantidade de itens coletados
        txtTotalItensColetados.text = $"x{totalItensColetados}";
        
        //atualizar o tempo no texto
        txtTempoDeJogo.text = $"{tempoDeJogo}";
        
        //atualizar o texto com o total de itens coletados no level
        txtTotalItensColetadosFinal.text = $"x{totalItensColetados}";
        
        //exibir o painel do topo e ocultar o painel de level completado
        pnlTopo.SetActive(true);
        pnlLevelCompletado.SetActive(false);
        
        //pegar a quantidade de itens coletaveis no level
        qtdDeItensColetaveisNoLevel = FindObjectsByType<ItemColetavel>(FindObjectsSortMode.None).Length;
    }

    // Update is called once per frame
    void Update()
    {
        //contar o tempo do jogo
        ContarTempo();
        
    }

    //metodo para decrementar a vida do jogador na cena
    public void DecrementarVidaJogador()
    {
        totalVidas--; //decrementar a vida
        //verificar se o jogador ainda tem vidas para continuar
        if (totalVidas == 0)
        {
            //finalizar o jogo
            FimDeJogo();
        }
        else
        {
            //atualizar a imagem da vida para o sprite correspondente
            imgVida.sprite = sptVida[totalVidas];
        }
    }
    //metodo para finalizar o jogo
    public void FimDeJogo()
    {
        //Dizer que acabou o jogo
        fimDeJogo = true;
        
        //zerar as vidas do jogador
        totalVidas = 0;
        
        //atualizar a imagem da vida para o sprite correspondente
        imgVida.sprite = sptVida[totalVidas];
        
        //desabilitar as funções do jogador
        playerControlador.DanoPlayer.MatarJogador();
        
        //contar o tempo para reiniciar o jogo
        StartCoroutine(ReiniciarLevel());
    }
    
    IEnumerator ReiniciarLevel()
    {
        //Aguardar 3 segundos para reiniciar o jogo
        yield return new WaitForSeconds(1f);
        //reiniciar o jogo
        ReiniciarLevelAtual();
    }
    /// <summary>
    /// Reiniciar cena atual do jogo
    /// </summary>
    public void ReiniciarLevelAtual()
    {
        //reiniciar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
    /// <summary>
    /// metodo para incrementar um item ao total de itens coletados
    /// </summary>
    public void IncrementarItensColetados()
    {
        //incrementar um item na variavel
        totalItensColetados++;
        
        //atualizar o texto
        txtTotalItensColetados.text = $"x{totalItensColetados}";
    }

    /// <summary>
    /// metodo para contar o tempo de jogo
    /// </summary>
    public void ContarTempo()
    {
        //verificar se o jogo ainda nao acabou
        if(fimDeJogo==true)return;
        
        //decrementar o tempo na variavel
        tempoDeJogo -= Time.deltaTime;
        
        //verificar se o tempo acabou
        if (tempoDeJogo <= 0)
        {
            //finalizar o jogo
            FimDeJogo();
        }
        else
        {
            //atualizar o texto sem aparecer decimais
            txtTempoDeJogo.text = $"{(int)tempoDeJogo}";
            
        }
    }
    
    /// <summary>
    /// metodo para finalizar o level
    /// </summary>
    public void CompletouLevel()
    {
        fimDeJogo = true;
        
        //congelar o player
        playerControlador.MovimentarPlayer.CongelarPlayer();
        
        //Exibir tela final do level
        StartCoroutine(ExibirTelaDoLevelCompletado());
    }
    
    /// <summary>
    /// Contar o tempo para poder exibir a tela de level completado e fazer a contagem das frutas
    /// </summary>
    /// <returns></returns>
    IEnumerator ExibirTelaDoLevelCompletado()
    {
        yield return new WaitForSeconds(3f);
        
        //calcular a medalha obtida no level
        CalcularMedalhaLevel(); 
        
        //exibir a tela do level completado e ocultar o painel do topo
        pnlTopo.SetActive(false);
        pnlLevelCompletado.SetActive(true);
        
        //fazer uma contagem dos itens coletados
        int contagem = 0;
        while (contagem < totalItensColetados)
        {
            //incrementar a contagem
            contagem++;
            
            //exibir essa contagem no texto
            txtTotalItensColetadosFinal.text = $"x{contagem}";
            
            //aguardar 0.1 segundo para reiniciar o loop e contar novamente os itens coletados
            yield return new WaitForSeconds(0.1f);
                
        }
    }
    
    /// <summary>
    /// metodo para calcular a medalha obtida no level
    /// </summary>
    private void CalcularMedalhaLevel()
    {
        //definir a porcentagem da coleta dos itens coletaveis
        float porcentagem = (float)totalItensColetados / (float)qtdDeItensColetaveisNoLevel * 100;
        
        //calcular a medalha
        idMedalha = porcentagem <50 ? 1 : porcentagem < 100 ? 2 : 3;
        
        //atribuir a medalha na imagem do icone
        imgIconeMedalha.sprite = sptsMedalhas[idMedalha];
    }
}