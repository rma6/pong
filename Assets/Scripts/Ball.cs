using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //speed: velocidade da bola, radius: raio da bola
    float speed, radius;

    //direction: vetor com direção da bola
    Vector2 direction;

    public bool hasLeafDiamond = false;

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
            GameOver.rigthWins = true;
            GameOver.gameover = true;

            //congela o jogo e desabilita o script
        }
        else if(transform.position.x > GameManager.topRight.x - radius)
        {
            //direita perde
            GameOver.rigthWins = false;
            GameOver.gameover = true;

            //congela o jogo e desabilita o script
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //caso colida com um pad inverte a direção do movimento
        if(collision.tag == "Pad")
        {
            //para colidir com um pad a bola tem que ter saido do diamante
            hasLeafDiamond = true;

            bool isRight = collision.GetComponent<Pad>().isRight;

            if((isRight && direction.x > 0) || ((!isRight && direction.x < 0)))
            {
                direction.x = -direction.x;

                //atividade 5.3 a direção que a bola vai depende de onde ela bate no pad
                direction.y = Math.Abs(transform.position.y - collision.GetComponent<Transform>().position.y);
                direction = direction.normalized;
            }
        }
        //caso colida com o diamande reflete o movimento
        else if(collision.tag == "Diamond" && hasLeafDiamond)
        {
            //primeiro quadrante
            if(transform.position.x >= 0 && transform.position.y >= 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(1,1)).normalized;
            }
            //segundo quadrante
            else if (transform.position.x < 0 && transform.position.y >= 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(-1, 1)).normalized;
            }
            //terceiro quadrante
            else if (transform.position.x < 0 && transform.position.y < 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(-1, -1)).normalized;
            }
            //quarto quadrante
            else if (transform.position.x >= 0 && transform.position.y < 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(1, -1)).normalized;
            }

        }
    }
}
