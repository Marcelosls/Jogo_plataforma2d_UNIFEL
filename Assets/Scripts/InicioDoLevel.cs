using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InicioDoLevel : MonoBehaviour
{
    private GameObject player;//gameobject do player
    public GameObject posicaoInicialPlayer;//posicao inicial do player
    // Start is called before the first frame update
    void Start()
    {
        //achar o player e armazenar na variavel
        player = FindFirstObjectByType<PlayerControlador>().GameObject();
        
        //mover o player para a posicao inicial
        player.transform.position = posicaoInicialPlayer.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
