using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilPlanta : MonoBehaviour
{
    public float velocidade;

    private Vector3 direcao;
    private bool houveColisao;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        //movimentar o projetil em relação a direção onde ele dever ir
        transform.Translate(direcao * (velocidade * Time.deltaTime));
    }
    
    /// <summary>
    /// mudar a direção que o projetil deve ir
    /// </summary>
    public void MudarDirecao(Vector3 novaDirecao)
    {
        direcao = novaDirecao;
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        //verificar se colidiu com o layer 7 que representa o player
        if(colisao.gameObject.layer==7 && houveColisao == false)
        {
            //diz que houve colisão
            houveColisao = true;
            
            //efetuar um dano ao jogador
            FindFirstObjectByType<PlayerControlador>().DanoPlayer.EfetuarDano();
        }
        Destroy(gameObject);
    }
}
