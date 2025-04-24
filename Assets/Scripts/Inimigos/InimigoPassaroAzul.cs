using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPassaroAzul : MonoBehaviour
{
    public GameObject passaroAzul;
    public float velocidade;
    public Vector3 posicaoFinal;
    private SpriteRenderer corpoPassaroAzul;
    private Vector3 posicaoInicial;
    private Vector3 posicaoAlvo;
    private bool estaMorto;
    private Animator animator;
    
    void Start()
    {
        posicaoInicial = passaroAzul.transform.position;
        posicaoFinal.y = posicaoInicial.y; // Mantém a altura fixa
        posicaoAlvo = posicaoFinal;
    
        animator = GetComponent<Animator>();
        corpoPassaroAzul = GetComponent<SpriteRenderer>();

        // Desativar a gravidade caso o pássaro tenha um Rigidbody2D
        Rigidbody2D rb = passaroAzul.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }

    void Update()
    {
        MovimentarPassaro();
        CalcularDistanciaAlvo();
    }
    
    private void MovimentarPassaro()
    {
        passaroAzul.transform.position = Vector3.MoveTowards(
            passaroAzul.transform.position,
            posicaoAlvo,
            velocidade * Time.deltaTime
        );
    }

    private void CalcularDistanciaAlvo()
    {
        if (Vector3.Distance(passaroAzul.transform.position, posicaoAlvo) < 0.001f)
        {
            if (!corpoPassaroAzul.flipX)
            {
                posicaoAlvo = posicaoInicial;
            }
            else
            {
                posicaoAlvo = posicaoFinal;
            }
            corpoPassaroAzul.flipX = !corpoPassaroAzul.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if(colisao.gameObject.CompareTag("Player") && !estaMorto)
        {
            estaMorto = true;
            colisao.gameObject.GetComponent<MovimentarPlayer>().ArremessarPlayer();
            animator.SetTrigger("Morte");
        }
    }
    
    public void DestruirPassaro()
    {
        Destroy(passaroAzul);
    }
}
