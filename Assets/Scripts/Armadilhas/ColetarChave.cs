using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetarChave : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private bool coletouChave = false;

    public bool ColetouChave
    {
        get { return coletouChave; }
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "Player" && coletouChave == false)
        {
            coletouChave = true;

            spriteRenderer.enabled = false;
        }
    }
}
