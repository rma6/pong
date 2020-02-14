using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pad pad;
    public Ball ball;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    void Start()
    {
        //captura limites da cena
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //cria instancia de bola
        Instantiate(ball);

        //cria instancias dos pads 
        Pad padR = Instantiate(pad) as Pad;
        padR.Init(true);

        Pad padL = Instantiate(pad) as Pad;
        padL.Init(false);
    }

    void Update()
    {

    }
}
