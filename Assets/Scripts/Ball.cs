using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //speed: velocidade da bola, radius: raio da bola
    float speed, radius;

    //direction: vetor com direção da bola
    Vector2 direction;

    void Start()
    {
        //inicia variáveis
        direction = Vector2.one.normalized; //todo: trocar para vetor aleatório
        radius = transform.localScale.x / 2;
        speed = 10.0f;
    }

    void Update()
    {
        //move a bola
        transform.Translate(direction * Time.deltaTime * speed);


        //inverte a direção da bola caso colida com o topo ou fundo da cena
        if (transform.position.y < GameManager.bottomLeft.y + radius || transform.position.y > GameManager.topRight.y - radius)
        {
            direction.y = -direction.y;
        }

        //detecta game over
        if(transform.position.x < GameManager.bottomLeft.x + radius)
        {
            //esquerda perde

            //congela o jogo e desabilita o script
            Time.timeScale = 0;
            enabled = false;
        }
        else if(transform.position.x > GameManager.topRight.x - radius)
        {
            //direita perde

            //congela o jogo e desabilita o script
            Time.timeScale = 0;
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //caso colida com um pad inverte a direção do movimento
        if(collision.tag == "Pad")
        {
            bool isRight = collision.GetComponent<Pad>().isRight;

            if((isRight && direction.x > 0) || ((!isRight && direction.x < 0)))
            {
                direction.x = -direction.x;
            }
        }
    }
}
