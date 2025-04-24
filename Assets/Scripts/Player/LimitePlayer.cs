using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimitePlayer : MonoBehaviour
{
    public bool estaNoLimite; //Diz se player chegou no limite

    private void OnTriggerStay2D(Collider2D colisao)
    {
        //Verificar se o limite chegou nos objetos cujo o layer é o limite
        if (colisao.gameObject.layer == 6)
        {
            estaNoLimite = true;}
    }

    private void OnTriggerExit2D(Collider2D colisao)
    {
        if (colisao.gameObject.layer == 6)
        {
            estaNoLimite = false;
        }
    }
}
