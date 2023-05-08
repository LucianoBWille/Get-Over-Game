using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildGameMode : MonoBehaviour
{
    public GameObject playerPrefab;
    public Canvas canvasPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        // instanciar player(s) e canvas de acordo com o gameMode definido em gameSettings.cs
        if("Multi" == GameSettings.gameMode){
            // get player1 from scene 
            GameObject player1 = GameObject.Find("Player");
            // instanciar player e canvas para multiplayer
            GameObject player2 = Instantiate(playerPrefab, new Vector3(2, 0, 0), Quaternion.identity);
            Canvas canvas2 = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // atribuir txtScore, txtHighScore, txtJumpDirection, txtJumpForce e txtWin do canvas para as variveis dos players
            player2.GetComponent<PlayerController>().txtScore = canvas2.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
            player2.GetComponent<PlayerController>().txtHighScore = canvas2.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>();
            player2.GetComponent<PlayerController>().txtDirection = canvas2.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>();
            player2.GetComponent<PlayerController>().txtForce = canvas2.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>();
            player2.GetComponent<PlayerController>().txtWin = canvas2.transform.GetChild(4).GetComponent<UnityEngine.UI.Text>();
            // configurar render camera do canvas para cada player (usar o main camera de cada player)
            canvas2.GetComponent<Canvas>().worldCamera = player2.transform.Find("Main Camera").GetComponent<Camera>();
            // configurar camera do player 1 para a viewport rect da metade esquerda da tela
            player1.transform.Find("Main Camera").GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            // configurar camera do player 2 para a viewport rect da metade direita da tela
            player2.transform.Find("Main Camera").GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            // atribuir tag "Player1" para player 1 e tag "Player2" para player 2
            player1.tag = "Player1";
            player2.tag = "Player2";
            // atribuir pauseMenu para o player 2
            player2.GetComponent<PlayerController>().pauseMenu = player1.GetComponent<PlayerController>().pauseMenu;
            // configurar o size da camera para 13
            player1.transform.Find("Main Camera").GetComponent<Camera>().orthographicSize = 13;
            player2.transform.Find("Main Camera").GetComponent<Camera>().orthographicSize = 13;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
