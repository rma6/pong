using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    //speed: velocidade do pad, height: altura do pad
    float speed, height;

    //isRight: indica se o pad é o da direita ou esquerda
    public bool isRight;

    //input: string com o nome do eixo para input
    string input;
    void Start()
    {
        //inicialização das variáveis independentes do lado do pad
        speed = 10.0f;
        height = transform.localScale.y;
    }

    void Update()
    {
        //calcula valor com base no input para mover o pad
        float inputValue = Input.GetAxis(input) * Time.deltaTime * speed;

        //limita o movimento do pad para dentro da tela
        if((transform.position.y < (GameManager.bottomLeft.y + height/2) && inputValue < 0) || (transform.position.y > (GameManager.topRight.y - height/2) && inputValue > 0))
        {
            inputValue = 0;
        }

        //aplica o movimento
        transform.Translate(inputValue * Vector2.up);
    }

    //inicia as variáveis dependentes do lado do pad
    public void Init(bool isRightPad)
    {
        isRight = isRightPad;
        Vector2 position = new Vector2(0, 0);

        //coloca o pad no local devido na cena
        if(isRightPad)
        {
            position = new Vector2(GameManager.topRight.x - transform.localScale.x, 0);
            input = "PadRight";

            //muda cor para vermelho
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            position = new Vector2(GameManager.bottomLeft.x + transform.localScale.x, 0);
            input = "PadLeft";

            //muda cor para azul
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        transform.position = position;

        transform.name = input;
    }
}
