using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public int xDirection, yDirection, xAngle, yAngle;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //xDirection = yDirection = xAngle = yAngle = 1;
        speed = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(xDirection, yDirection) * speed * Time.deltaTime);
    }

    private void MoveBall()
    {

    }

    public void SetBallDirection(BallMoviments move)
    {
        switch (move)
        {
            case BallMoviments.STRAIGHTRIGHT:
                xDirection = 1;
                yDirection = 0;
                break;

            case BallMoviments.STRAIGHTLEFT:
                xDirection = -1;
                yDirection = 0;
                break;

            case BallMoviments.UP30RIGHT:
                break;

            case BallMoviments.UP45RIGHT:
                xDirection = yDirection = 1;
                break;

            case BallMoviments.UP60RIGHT:
                break;

            case BallMoviments.UP30LEFT:
                break;

            case BallMoviments.UP45LEFT:
                xDirection = -1;
                yDirection = 1;
                break;

            case BallMoviments.UP60LEFT:
                break;

            case BallMoviments.DOWN30RIGHT:
                break;

            case BallMoviments.DOWN45RIGHT:
                xDirection = 1;
                yDirection = -1;
                break;

            case BallMoviments.DOWN60RIGHT:
                break;

            case BallMoviments.DOWN30LEFT:
                break;

            case BallMoviments.DOWN45LEFT:
                xDirection = yDirection = -1;
                break;

            case BallMoviments.DOWN60LEFT:
                break;

            default:
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
            yDirection = -yDirection;

        if (collision.gameObject.tag == "Player")
        {
            xDirection = -xDirection;
            speed += 0.5f;
        }
    }
}
