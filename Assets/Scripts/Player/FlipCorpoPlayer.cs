using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCorpoPlayer : MonoBehaviour
{
    public SpriteRenderer spriteCorpo;
    /// <summary>
    /// Fazer o corpo virar para a direita
    /// </summary>
    public void OlharDireita()
    {
        spriteCorpo.flipX = false;
    }
    /// <summary>
    /// fazer o corpo olhar para esquerda
    /// </summary>
    public void OlharEsquerda()
    {
        spriteCorpo.flipX = true;

    }
    
}