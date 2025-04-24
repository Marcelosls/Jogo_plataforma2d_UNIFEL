using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoSpike : MonoBehaviour
{
    //private bool houveColisao; //verificar se ouve uma colisão com o spike

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se colidiu com o player
        if (colisao.gameObject.tag == "Player")
        {
            //efetuar um dano ao jogador
            colisao.gameObject.GetComponent<DanoPlayer>().EfetuarDano();
        }
        
    }
}
