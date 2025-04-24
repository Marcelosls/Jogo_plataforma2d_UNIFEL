using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPlanta : MonoBehaviour
{
    private SpriteRenderer corpoPlanta;

    private Animator animator;

    public float tempoDeTiro;
    private float tempoDeEspera;

    public GameObject projetil;

    private bool estaMorto;
    // Start is called before the first frame update
    void Start()
    {
        corpoPlanta = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        tempoDeEspera = Time.time + tempoDeTiro;
    }

    // Update is called once per frame
    void Update()
    {
        //verificar se pode atirar
        if (Time.time > tempoDeEspera)
        {
            AtualizaTempoDeEspera();
            animator.SetTrigger("Ataque");
            
        }
    }
    
    private void AtualizaTempoDeEspera()
    {
        tempoDeEspera = Time.time + tempoDeTiro;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// metodo para atirar o projetil atravez da animação da planta
    /// </summary>
    public void AtirarProjetil()
    {
        //Debug.Log("Disparando projetil!");

        //instanciar o projetil
        GameObject novoProjetil = Instantiate(projetil);
        
        //Verificar para aonde a planta esta olhando para pode lançar o projetil na direção correta
        if (corpoPlanta.flipX == true)
        {
            PosicionarProjetil(novoProjetil, 0.7f, Vector3.right);
        }else
        {
            PosicionarProjetil(novoProjetil, -0.5f, Vector3.left);
            }
        
    }

    private void PosicionarProjetil(GameObject novoProjetil, float diferencax, Vector3 direcao)
    {
        //posicionar o projetil no lado direito da planta
        novoProjetil.transform.position = new Vector3(
            transform.position.x + diferencax,
            transform.position.y +0.14f,
            0);
            
        //definir a direção de movimentação do projetil
        novoProjetil.GetComponent<ProjetilPlanta>().MudarDirecao(direcao);

    }
    
    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "Player" && estaMorto == false)
        {
            //dizer que a plantan morreu
            estaMorto = true;
            
            //Efetuar dano no player
            colisao.gameObject.GetComponent<DanoPlayer>().EfetuarDano();
            
            //ativa animação de morte
            animator.SetTrigger("Morte");
            animator.SetBool("EstaMorto", true);
        }

    }

    
    private void DestruirPlanta()
    {
        Destroy(gameObject);
    }
}
