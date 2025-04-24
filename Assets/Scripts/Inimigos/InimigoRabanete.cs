using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class InimigoRabanete : MonoBehaviour
{
    public float velocidade;
    private SpriteRenderer corpoRabanete;
    private Animator animator;
    private bool estaParado;
    private bool houveColisao;
    private string animacaoAtual;
    private float tempoDeEspera = 3f;
    private float tempoAguardando;
    
    
    // Start is called before the first frame update
    void Start()
    {
        corpoRabanete = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animacaoAtual = "Correndo";
    }

    // Update is called once per frame
    void Update()
    {
        //verificar se está parado
        if (estaParado == true)
        {
            if(Time.time > tempoAguardando)
            {
                VirarRabanete();
            }
            return;
        }
        transform.Translate(Vector3.left*velocidade*Time.deltaTime);
    }


    void OnTriggerExit2D(Collider2D colisao)
    {
        //Verificar se rabanete deixou de colidir com o chão
        if (colisao.gameObject.layer == 6)
        {
            //ativa animação de parado
            animator.SetTrigger("Parado");
            animacaoAtual="Parado";
            tempoAguardando = Time.time + tempoDeEspera;
            //diz que rabanete parou
            estaParado = true;
        }
    }
    
    private void VirarRabanete()
    {
        //inverter velocidade para poder se mover no sentido contrario
        velocidade *= -1;
        corpoRabanete.flipX = !corpoRabanete.flipX;
        estaParado=false;
        animator.SetTrigger("Correndo");
        animacaoAtual="Correndo";
        houveColisao= false;
    }
    
    private void DanoRabanete()
    {
        //efetuar dano ao player
        FindFirstObjectByType<PlayerControlador>().DanoPlayer.EfetuarDano();
        animator.SetTrigger("Dano");
        houveColisao = true;
    }

    private void AtivaAnimacaoPosDano()
    {
        animator.SetTrigger(animacaoAtual);
        houveColisao = false;
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        //verificar se o jogo acabou
        if (CanvasGameMng.Instance.fimDeJogo==true) return;
        if(colisao.gameObject.layer == 7 && houveColisao == false)
        {
            DanoRabanete();
        }
    }
}
