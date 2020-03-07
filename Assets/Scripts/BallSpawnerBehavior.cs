using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerBehavior : MonoBehaviour
{

    public GameObject[] spawners;

    [SerializeField]
    private GameObject ball;

    private BallBehavior ballBehavior;

    // Start is called before the first frame update
    void Start()
    {
        LaunchBallRandomly();
    }

    void Update()
    {
        if (ballBehavior.IsToRestart)
        {
            ballBehavior.IsToRestart = false;
            ballBehavior.RestartSpeed();
            ball.transform.position = spawners[0].transform.position;
            LaunchBallByScorer(ballBehavior.PlayerScorer);
        }
    }

    private void LaunchBallByScorer(int player)
    {
        RestartBallPosition();

        BallMoviments moviment;

        if (player == 1)
            moviment = BallMoviments.DOWN45RIGHT;
        else
            moviment = BallMoviments.DOWN45LEFT;

        InitiateBall();

        ballBehavior.SetBallDirection(moviment);
    }

    public void LaunchBallRandomly()
    {
        BallMoviments moviment;
        int rand = Random.Range(0, 2);

        if (rand == 0)
            moviment = BallMoviments.DOWN45LEFT;
        else
            moviment = BallMoviments.DOWN45RIGHT;

        InitiateBall();

        ballBehavior.SetBallDirection(moviment);
    }

    private void InitiateBall()
    {
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
            ball = Instantiate(ball, spawners[ChooseBallSpawner()].transform);
        else
            ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball.activeSelf == false) ball.SetActive(true);
        RestartBallPosition();
        ballBehavior = ball.GetComponent<BallBehavior>();
    }

    private int ChooseBallSpawner()
    {
        if (spawners.Length <= 0)
            throw new System.Exception("Spawners not initialized.");

        int spawnerIndex = Random.Range(0, spawners.Length - 1);

        return spawnerIndex;
    }

    public GameObject GetBallInstance()
    {
        return ball;
    }

    private void RestartBallPosition()
    {
        ball.transform.position = spawners[0].transform.position;
    }
}
