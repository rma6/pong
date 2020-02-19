using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pad pad;
    public Ball ball;
    public Diamond diamond;

    Pad padR;
    Pad padL;
    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static bool start = false;

    Vector3 padScaleTarget = new Vector3(0.5f, 2.0f, 0);
    Vector3 ballScaleTarget = new Vector3(0.5f, 0.5f, 0);

    void Start()
    {
        print(Screen.width);
        print(Screen.height);
        //captura limites da cena
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //cria instancia de bola
        ball = Instantiate(ball) as Ball;

        //cria instancias dos pads 
        padR = Instantiate(pad) as Pad;
        padR.Init(true);

        padL = Instantiate(pad) as Pad;
        padL.Init(false);

        //cria instancia de diamond
        diamond = Instantiate(diamond) as Diamond;

        //animação
        ball.enabled = false;
        padL.enabled = false;
        padR.enabled = false;
        ball.transform.localScale = new Vector3(500.0f, 500.0f, 0);
        padL.transform.localScale = new Vector3(100.0f, 100.0f, 0);
        padR.transform.localScale = new Vector3(100.0f, 100.0f, 0);
    }

    void Update()
    {
        //animação
        if(!start && !GameOver.gameover)
        {
            ball.transform.localScale += (ballScaleTarget - ball.transform.localScale) * 0.1f;
            padL.transform.localScale += (padScaleTarget - padL.transform.localScale) * 0.1f;
            padR.transform.localScale += (padScaleTarget - padR.transform.localScale) * 0.1f;
            if((ball.transform.localScale-ballScaleTarget).magnitude < 0.01f)
            {
                start = true;
                ball.enabled = true;
                padL.enabled = true;
                padR.enabled = true;
            }
        }
        if(GameOver.gameover)
        {
            ball.enabled = false;
            padL.enabled = false;
            padR.enabled = false;
            Vector3 ballScaleTarget = new Vector3(0, 0, 0);
        }
        if(start && GameOver.gameover)
        {
            ballScaleTarget = new Vector3(60.0f, 60.0f, 0);
            ball.transform.localScale += (ballScaleTarget - ball.transform.localScale) * 0.03f;
            if ((ball.transform.localScale - ballScaleTarget).magnitude < 1.0f)
            {
                Reset();
            }
        }

    }

    void Reset()
    {
        start = false;
        GameOver.gameover = false;
        ball.enabled = false;
        padL.enabled = false;
        padR.enabled = false;


        ball.hasLeafDiamond = false;
        ball.transform.position = new Vector2(0, 0);

        padR.Init(true);
        padL.Init(false);

        //animação
        ball.transform.localScale = new Vector3(500.0f, 500.0f, 0);
        padL.transform.localScale = new Vector3(100.0f, 100.0f, 0);
        padR.transform.localScale = new Vector3(100.0f, 100.0f, 0);
        padScaleTarget = new Vector3(0.5f, 2.0f, 0);
        ballScaleTarget = new Vector3(0.5f, 0.5f, 0);
    }
}
