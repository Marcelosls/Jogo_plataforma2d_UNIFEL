using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeEspinhos : MonoBehaviour
{
    
    public float velocidade = 100;

    public bool rotacaoconstante;
    

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.back * velocidade *Time.deltaTime;
        
        if(rotacaoconstante == false)
        {
            if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270)
            {
                velocidade*=-1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag=="Player")
        {
            CanvasGameMng.Instance.FimDeJogo();
        }
    }
}
