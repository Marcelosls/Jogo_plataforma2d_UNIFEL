using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarChefe : MonoBehaviour
{
    private ChefeControlador chefeControlador;
    
    private SpriteRenderer spriteRenderer;

    public float velocidade;
    
    // Start is called before the first frame update
    void Start()
    {
        chefeControlador = GetComponent<ChefeControlador>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chefeControlador.EstaMovendo == false) return;
        
        Movimentar();
    }

    private void Movimentar()
    {
        if (spriteRenderer.flipX == false)
        {
            transform.Translate(Vector3.left * velocidade * Time.deltaTime);
        }
        else if(spriteRenderer.flipX == true)
        {
            transform.Translate(Vector3.right * velocidade * Time.deltaTime);
        }
    }

    public void FlipCorpo()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;

    }
}
