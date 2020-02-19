using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool gameover = false;
    public static bool rigthWins = false;
    void Start()
    {
        
    }

    void Update()
    {
        //mostra texto quando um jogador perde
        if(gameover)
        {
            if(rigthWins)
            {
                GetComponent<Text>().text = "Right Wins!";
            }
            else
            {
                GetComponent<Text>().text = "Left Wins!";
            }
        }
        else
        {
            GetComponent<Text>().text = "";
        }
    }
}
