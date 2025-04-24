using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    //declarar as variaveis que vão armazenar os codigos do player
    private MovimentarPlayer movimentarPlayer;
    private AnimacaoPlayer animacaoPlayer;
    private DanoPlayer danoPlayer;
    
    //declarar as propriedades de acesso as variaveis do player
    public MovimentarPlayer MovimentarPlayer
    {
        get { return movimentarPlayer; }
    }
    public AnimacaoPlayer AnimacaoPlayer
    {
        get { return animacaoPlayer; }
    }
    public DanoPlayer DanoPlayer
    {
        get { return danoPlayer; }
    }

    private void Awake()
    {
        //obter a referencia do movimentar player
        movimentarPlayer = GetComponent<MovimentarPlayer>();
        
        //obter a referencia do animacao player
        GetComponentInChildren<AnimacaoPlayer>();
        
        //obter a referencia do dano player
        danoPlayer = GetComponentInChildren<DanoPlayer>();
    }
}
