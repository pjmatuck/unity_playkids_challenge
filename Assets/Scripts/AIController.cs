﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{ 
    public float speed;
    public BallSpawnerBehavior spawner;

    private Transform ball;

    int decision;
    bool toDecide = true;
    bool collided = false;

    void Start()
    {
        ball = spawner.GetBallInstance().transform;
    }

    void FixedUpdate()
    {
        //Choose AI Strategy
        if (toDecide)
        {
            decision = Random.Range(0, 2);
            Debug.Log("AI decision: " + decision);
            toDecide = false;
        }

        switch (decision)
        {
            /*Strategy: FOLLOWTHEBALL
             * The AI player follow the ball all the time.
             */
            case 0:
                if (ball.position.y > transform.position.y) MovePaddle(1);
                if (ball.position.y < transform.position.y) MovePaddle(-1);
                break;
            /*Strategy: WAITTOATTACK
             * The AI player wait the ball cross the midfield to follow it.
             */
            case 1:
                if (ball.position.x >= 0)
                {
                    if (ball.position.y > transform.position.y) MovePaddle(1);
                    if (ball.position.y < transform.position.y) MovePaddle(-1);
                }
                break;
            default:
                break;
        }
        
        // Keep the last decision until the follow conditions
        if (collided || ball.position.x > transform.position.x + 0.5)
        {
            if (collided) collided = false;
            toDecide = true;
        }
    }

    private void MovePaddle(float input)
    {
        transform.Translate(new Vector3(0.0f, speed * Time.deltaTime * input));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
            collided = true;
    }
}
