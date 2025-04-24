using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    
    public Animator animator;

    private bool coletouItem;//variavel para saber se o item foi coletado
    
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D colisao)
    {
        //verificar se foi o player que colidiu e se ja houve uma colisão com ele anteriormente
        if (coletouItem == false && colisao.gameObject.tag == "Player")
        {
            coletouItem = true;
            //ativar a animação de coletar o item
            animator.SetTrigger("Coletar");

            //incrementar a coleta do item
            CanvasGameMng.Instance.IncrementarItensColetados();
            //destruir o item
        }
    }

    /// <summary>
    /// metodo para poder destruir o item apos o fim da animação de coleta
    /// </summary>
    public void DestruirItem()
        {
            Destroy(gameObject);
        }
    
}
