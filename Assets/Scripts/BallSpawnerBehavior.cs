using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerBehavior : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        //LaunchBallByScorer(1);
        LaunchBallRandomly();
    }

    private void LaunchBallByScorer(int player)
    {
        BallMoviments moviment;

        if (player == 1)
            moviment = BallMoviments.UP45LEFT;
        else
            moviment = BallMoviments.UP45RIGHT;

        ball = Instantiate(ball, spawners[0].transform);
        ball.GetComponent<BallBehavior>().SetBallDirection(moviment);
    }

    private void LaunchBallRandomly()
    {
        BallMoviments moviment;
        int rand = Random.Range(0, 2);

        if (rand == 0)
            moviment = BallMoviments.DOWN45LEFT;
        else
            moviment = BallMoviments.DOWN45RIGHT;

        ball = Instantiate(ball, spawners[0].transform);
        ball.GetComponent<BallBehavior>().SetBallDirection(moviment);
        
    }

    private int ChooseBallSpawner()
    {
        if (spawners.Length <= 0)
            throw new System.Exception("Spawners not initialized.");

        int spawnerIndex = Random.Range(0, spawners.Length - 1);

        return spawnerIndex;
    }
}
