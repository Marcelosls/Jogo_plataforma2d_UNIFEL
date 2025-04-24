using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortao : MonoBehaviour
{
    public ColetarChave chave;
    public float velocidade;
    public GameObject fechadura;
    private Quaternion rotacaoAlvo;
    private bool abriuPortao;

    // Start is called before the first frame update
    void Start()
    {
        rotacaoAlvo = Quaternion.Euler(new Vector3(0, 90, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (abriuPortao == true)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                rotacaoAlvo,
                velocidade * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag=="Player" && chave.ColetouChave == true && abriuPortao == false)
        {
            abriuPortao = true;
            fechadura.SetActive(false);
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            Destroy(boxCollider2D);
        }
    }
}
