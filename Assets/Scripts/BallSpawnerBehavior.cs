using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerBehavior : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject ball;

    private BallBehavior ballBehavior;

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

    //Put ball on game depending on the scorer player.
    private void LaunchBallByScorer(int player)
    {
        RestartBallPosition();

        BallMoviments moviment;

        if (player == 2)
            moviment = BallMoviments.DOWN45RIGHT;
        else
            moviment = BallMoviments.DOWN45LEFT;

        InitiateBall();

        ballBehavior.SetBallDirection(moviment);
    }

    //Put ball on game randomly.
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

    //Ball Start configuration 
    private void InitiateBall()
    {
        ball.transform.position = spawners[ChooseBallSpawner()].transform.position;

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

    //Return used ball instance for others scripts
    public GameObject GetBallInstance()
    {
        return ball;
    }

    private void RestartBallPosition()
    {
        ball.transform.position = spawners[0].transform.position;
    }
}
