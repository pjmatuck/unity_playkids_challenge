using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text player1Score, player2Score;
    public GameObject EndGamePanel;

    private static GameManager _gameManagerinstance;

    int p1Score, p2Score;
    public int P1Score { get { return p1Score; } }
    public int P2Score { get { return p2Score; } }

    private void Awake()
    {
        _gameManagerinstance = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        p1Score = 0;
        player1Score.text = p1Score.ToString();
        p2Score = 0;
        player2Score.text = p2Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartMainScene();

        if (p1Score == 5 || p2Score == 5) RestartMainScene();
            
    }

    public void AddPointToPlayer(int player)
    {
        if(player == 1)
        {
            p1Score++;
            player1Score.text = p1Score.ToString();
        } else
        {
            p2Score++;
            player2Score.text = p2Score.ToString();
        }
    }

    public static GameManager GetGameManagerInstace()
    {
        return _gameManagerinstance;
    }

    private void RestartMainScene()
    {
        SceneManager.LoadScene("_MainScene");
    }

    public void Restart()
    {

    }

    public void Quit()
    {

    }
}
