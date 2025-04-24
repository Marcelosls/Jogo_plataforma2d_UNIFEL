using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContatoPlataformaFlutuante : MonoBehaviour
{
    public Animator animator;
    public GameObject plataformaFlutuante;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = plataformaFlutuante.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = plataformaFlutuante.AddComponent<Rigidbody2D>();
        }
        rb.isKinematic = true; // Para evitar interações físicas antes do momento certo
    }

    private void OnTriggerExit2D(Collider2D colisao)
    {
        if (colisao.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetTrigger("Caindo");
            Destroy(plataformaFlutuante, 0.5f);
        }
    }
}