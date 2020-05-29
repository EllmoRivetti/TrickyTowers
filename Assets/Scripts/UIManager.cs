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
    Text current_powerup_value;
    [SerializeField]
    Text powerup_score_value;
    [SerializeField]
    GameObject loose_panel;

    void Update()
    {
        score_value.text = gameLoop.GetScore().ToString();
        current_health_value.text = gameLoop.GetCurrentHealth().ToString();
        powerup_score_value.text = gameLoop.GetNextScoreForPowerUp().ToString();

        string current_pwrup = gameLoop.GetCurrentPowerUp().ToString();
        if (current_pwrup != "None")
            current_powerup_value.text = current_pwrup;
        else
            current_powerup_value.text = "";

        if (gameLoop.GetCurrentHealth() <= 0)
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
