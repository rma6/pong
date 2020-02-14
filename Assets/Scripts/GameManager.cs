using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pad pad;
    public Ball ball;

    Pad padR;
    Pad padL;
    Ball ball_;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static bool start = false;

    Vector3 padScaleTarget = new Vector3(0.5f, 2.0f, 1.0f);
    Vector3 ballScaleTarget = new Vector3(0.5f, 0.5f, 0.5f);

    void Start()
    {
        //captura limites da cena
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //cria instancia de bola
        ball_ = Instantiate(ball);

        //cria instancias dos pads 
        padR = Instantiate(pad) as Pad;
        padR.Init(true);

        padL = Instantiate(pad) as Pad;
        padL.Init(false);

        //animação
        ball_.enabled = false;
        padL.enabled = false;
        padR.enabled = false;
        ball_.transform.localScale = new Vector3(500.0f, 500.0f, 500.0f);
        padL.transform.localScale = new Vector3(100.0f, 100.0f, 100.0f);
        padR.transform.localScale = new Vector3(100.0f, 100.0f, 100.0f);
    }

    void Update()
    {
        //animação
        if(!start && !GameOver.gameover)
        {
            ball_.transform.localScale += (ballScaleTarget - ball_.transform.localScale) * 0.1f;
            padL.transform.localScale += (padScaleTarget - padL.transform.localScale) * 0.1f;
            padR.transform.localScale += (padScaleTarget - padR.transform.localScale) * 0.1f;
            if((ball_.transform.localScale-ballScaleTarget).magnitude < 0.01f)
            {
                start = true;
                ball_.enabled = true;
                padL.enabled = true;
                padR.enabled = true;
            }
        }
        if(GameOver.gameover)
        {
            ball_.enabled = false;
            padL.enabled = false;
            padR.enabled = false;
            Vector3 ballScaleTarget = new Vector3(0, 0, 0);
        }
        if(start && GameOver.gameover)
        {
            ball_.transform.localScale -= (ballScaleTarget - ball_.transform.localScale) * 0.1f;
        }
    }
}
