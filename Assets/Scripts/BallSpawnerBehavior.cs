using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerBehavior : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject ball;

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
            ball.transform.position = spawners[0].transform.position;
            LaunchBallByScorer(ballBehavior.PlayerScorer);
        }
    }

    private void LaunchBallByScorer(int player)
    {
        BallMoviments moviment;

        if (player == 1)
            moviment = BallMoviments.DOWN45RIGHT;
        else
            moviment = BallMoviments.DOWN45LEFT;

        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0) InitiateBall();

        ballBehavior.SetBallDirection(moviment);
    }

    private void LaunchBallRandomly()
    {
        BallMoviments moviment;
        int rand = Random.Range(0, 2);

        if (rand == 0)
            moviment = BallMoviments.DOWN45LEFT;
        else
            moviment = BallMoviments.DOWN45RIGHT;

        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0) InitiateBall();

        ballBehavior.SetBallDirection(moviment);
        
    }

    private void InitiateBall()
    {
        ball = Instantiate(ball, spawners[0].transform);
        ballBehavior = ball.GetComponent<BallBehavior>();
    }

    private int ChooseBallSpawner()
    {
        if (spawners.Length <= 0)
            throw new System.Exception("Spawners not initialized.");

        int spawnerIndex = Random.Range(0, spawners.Length - 1);

        return spawnerIndex;
    }
}
