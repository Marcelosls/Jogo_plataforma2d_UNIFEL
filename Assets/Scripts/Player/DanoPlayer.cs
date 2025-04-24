using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPlayer : MonoBehaviour
{
    public MovimentarPlayer movimentarPlayer; // variavel para acessar os metodos e atributos de movimentação do player

    public AnimacaoPlayer animacaoPlayer; //Varivael para acessasr os metodos e atributos da animação do player
    
    /// <summary>
    /// Efetua um dano ao player
    /// </summary>
    public void EfetuarDano()
    {
        //ativar a animação de dano
        animacaoPlayer.PlayDano();
        
        //Resetar a fisica do jogador
        movimentarPlayer.ResetarFisicaDeMovimentacao();
        
        //arremessar o jogador
        movimentarPlayer.ArremessarPlayer();
        
        //decrementar a vida do jogador
        CanvasGameMng.Instance.DecrementarVidaJogador();
    }

    /// <summary>
    /// desabilita as fisicas e ativa a animação de morte
    /// </summary>
    public void MatarJogador()
    {
        //zerar a fisica do player
        movimentarPlayer.ResetarFisicaDeMovimentacao();
        
        //desabilitar a fisica do player
        movimentarPlayer.RemoverGravidade();
        
        //ativar a animação de morte
        animacaoPlayer.PlayMorte();
    }
    
}
