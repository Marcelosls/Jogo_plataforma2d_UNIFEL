using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaChefe : MonoBehaviour
{
    private ChefeControlador chefeControlador;
    // Start is called before the first frame update
    void Start()
    {
        chefeControlador = FindObjectOfType<ChefeControlador>();
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "Player")
        {
            chefeControlador.HabilitaMovimentacao();
            Destroy(gameObject);
        }
    }
}
