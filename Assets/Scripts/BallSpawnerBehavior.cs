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
        LaunchBallByScorer(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LaunchBallByScorer(int player)
    {
        if (player == 1)
        {
            Instantiate(ball, spawners[0].transform);
            ball.GetComponent<BallBehavior>().SetBallDirection(BallMoviments.UP45LEFT);
        } else
        {
            Instantiate(ball, spawners[0].transform);
            ball.GetComponent<BallBehavior>().SetBallDirection(BallMoviments.UP45RIGHT);
        }
    }

    private void LaunchBallRandomly()
    {

    }

    private int ChooseBallSpawner()
    {
        if (spawners.Length <= 0)
            throw new System.Exception("Spawners not initialized.");

        int spawnerIndex = Random.Range(0, spawners.Length - 1);

        return spawnerIndex;
    }
}
