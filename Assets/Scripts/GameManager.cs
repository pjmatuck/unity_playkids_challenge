using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //References to players
    private const int PLAYER1 = 1;
    private const int PLAYER2 = 2;

    public UIManager uiManager;
    public BallSpawnerBehavior ballSpawner;

    //Number of points to finish game
    public int pointToEndGame;

    //Delay for end game
    public float waitTimeForEndGame;
    private float timeToEnd;

    private static GameManager gameManagerInstance;
    public static GameState GameState { get; private set; }

    int winner;
    public int P1Score { get; private set; }
    public int P2Score { get; private set; }

    private void Awake()
    {
        gameManagerInstance = GetComponent<GameManager>();
    }

    void Start()
    {
        P1Score = 0;
        P2Score = 0;

        uiManager.UpdateGameScore(PLAYER1, P1Score);
        uiManager.UpdateGameScore(PLAYER2, P2Score);

        GameState = GameState.START;
    }

    void Update()
    {
        switch (GameState)
        {
            case GameState.START:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    uiManager.ShowPressStartScreen(false);
                    ballSpawner.LaunchBallRandomly();
                    GameState = GameState.RUNNING;
                }
                break;
            case GameState.RUNNING:
                if (P1Score == pointToEndGame || P2Score == pointToEndGame)
                {
                    timeToEnd += Time.deltaTime;
                    if(timeToEnd >= waitTimeForEndGame)
                    {
                        timeToEnd = 0;
                        GameState = GameState.END;
                        uiManager.ShowEndGameScreen(true, winner);
                    }
                }
                break;
            case GameState.END:
                break;
            default:
                break;
        }

        //For development mode
        //if (Input.GetKeyDown(KeyCode.R)) RestartMainScene();
    }

    //Update Player Score
    public void AddPointToPlayer(int player)
    {
        if(player == PLAYER1)
        {
            P1Score++;
            uiManager.UpdateGameScore(player, P1Score);
        } else
        {
            P2Score++;
            uiManager.UpdateGameScore(player, P2Score);
        }

        if (P1Score == pointToEndGame)
            winner = PLAYER1;
        else if (P2Score == pointToEndGame)
            winner = PLAYER2;
    }

    //Return GameManager instance for other classes
    public static GameManager GetGameManagerInstace()
    {
        return gameManagerInstance;
    }


    public void Restart()
    {
        RestartMainScene();
    }

    //Only works on mobile
    public void Quit()
    {
        Application.Quit(0);
    }

    private void RestartMainScene()
    {
        SceneManager.LoadScene("_MainScene");
    }
}

public enum GameState
{
    START,
    RUNNING,
    END
}
