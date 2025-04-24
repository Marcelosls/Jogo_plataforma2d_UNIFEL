using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPlayer : MonoBehaviour
{
    public Animator animator;
    
    /// <summary>
    /// ativa a animação de parado do player
    /// </summary>
    public void PlayParado()
    {
        animator.SetBool("Parado", true);
        animator.SetBool("Correndo", false);
        animator.SetBool("Pulando", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("DeslizarParede", false);
    }
    
    /// <summary>
    /// ativa a animação de parado do player
    /// </summary>
    public void PlayCorrendo()
    {
        animator.SetBool("Parado", false);
        animator.SetBool("Correndo", true);
        animator.SetBool("Pulando", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("DeslizarParede", false);
    }
    
    /// <summary>
    /// ativa a animação de pulando do player
    /// </summary>
    public void PlayPulando()
    {
        animator.SetBool("Pulando", true);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("DeslizarParede", false);
    }
    
    /// <summary>
    /// ativa a animação de caindo do player
    /// </summary>
    public void PlayCaindo()
    {
        animator.SetBool("Pulando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Caindo", true);
        animator.SetBool("DeslizarParede", false);
    }
    
    /// <summary>
    /// ativa a animação de deslizando do player
    /// </summary>
    public void PlayDeslizarParede()
    {
        animator.SetBool("Pulando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("DeslizarParede", true);
        animator.SetBool("Caindo", false);
        
    }
    
    /// <summary>
    /// ativa a animação de pulo duplo
    /// </summary>
    public void PlayPuloDuplo()
    {
        animator.SetTrigger("PuloDuplo");
        animator.SetBool("Pulando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Caindo", false);
    }
    /// <summary>
    /// Ativa animação de dano do player
    /// </summary>
    public void PlayDano()
    {
        animator.SetTrigger("Dano");
    }

    /// <summary>
    /// ativa a animação de morte do jogador
    /// </summary>
    public void PlayMorte()
    {
        animator.SetBool("Fim", true);
        animator.SetTrigger("Morte");
    }
    
}
