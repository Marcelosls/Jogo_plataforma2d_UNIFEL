using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimDoLevel : MonoBehaviour
{
    public Animator animator;
    
    void OnTriggerEnter2D(Collider2D colisao)
    {
        //verificar se a colisão foi com o player
        if (colisao.gameObject.tag == "Player")
        {
            //ativar a animação de fim de jogo
            animator.SetBool("FimDoLevel", true);
            
            //Finalizar o level
            CanvasGameMng.Instance.CompletouLevel();
        }
    }
}
