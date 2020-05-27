using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameLoop gameLoop;
    [SerializeField]
    Text score_value;
    [SerializeField]
    Text current_health_value;
    [SerializeField]
    GameObject loose_panel;


    // Update is called once per frame
    void Update()
    {
        score_value.text = gameLoop.GetScore().ToString();
        current_health_value.text = gameLoop.GetCurrentHealth().ToString();

        if(gameLoop.GetCurrentHealth() <= 0)
        {
            loose_panel.SetActive(true);
            gameLoop.EndGame();
        }
    }

    public void Reset()
    {
        loose_panel.SetActive(false);
        this.gameLoop.ResetGame();        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
