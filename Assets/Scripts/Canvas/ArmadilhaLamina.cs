using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilhaLamina : MonoBehaviour
{
    public Vector3[] destinos;
    public float velocidade;
    public float tempoEsperaProximoDestino = 1f;

    private int idProximoDestino;
    private bool chegouAoDestino;
    private float tempoProximoDestino = 0f;
    private int direcao = 1; // 1 pra frente, -1 pra trás

    void Start()
    {
        transform.position = destinos[0];
        idProximoDestino = 1;
    }

    void Update()
    {
        MovimentarLamina();   
    }

    private void MovimentarLamina()
    {
        if (chegouAoDestino)
        {
            if (Time.time > tempoProximoDestino)
            {
                idProximoDestino += direcao;

                // Inverte a direção se chegar no fim ou no início
                if (idProximoDestino >= destinos.Length || idProximoDestino < 0)
                {
                    direcao *= -1;
                    idProximoDestino += direcao * 2; // Corrige o índice após inverter
                }

                chegouAoDestino = false;
            }
        }
        else
        {
            float velocidadeMovimento = velocidade * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position, 
                destinos[idProximoDestino], 
                velocidadeMovimento
            );

            if (Vector3.Distance(transform.position, destinos[idProximoDestino]) < 0.001f)
            {
                tempoProximoDestino = Time.time + tempoEsperaProximoDestino;
                chegouAoDestino = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D Colisor)
    {
        if (Colisor.gameObject.tag == "Player")
        {
            CanvasGameMng.Instance.FimDeJogo();
        }
    }
}
