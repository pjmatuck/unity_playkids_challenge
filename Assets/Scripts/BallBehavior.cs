using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public float speed;
    public AudioClip[] audioClips;

    private float xDirection, yDirection;
    private AudioSource ballSounds;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2.5f;
        ballSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        transform.Translate(new Vector3(xDirection, yDirection) * speed * Time.deltaTime);
    }

    //Based on trigonometric table
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
                xDirection = Mathf.Sqrt(3) / 2;
                yDirection = 1f / 2;
                break;

            case BallMoviments.UP45RIGHT:
                xDirection = yDirection = 1;
                break;

            case BallMoviments.UP60RIGHT:
                xDirection = 1f / 2;
                yDirection = Mathf.Sqrt(3) / 2;
                break;

            case BallMoviments.UP30LEFT:
                xDirection = -Mathf.Sqrt(3) / 2;
                yDirection = 1f / 2;
                break;

            case BallMoviments.UP45LEFT:
                xDirection = -1;
                yDirection = 1;
                break;

            case BallMoviments.UP60LEFT:
                xDirection = -1f / 2;
                yDirection = Mathf.Sqrt(3) / 2;
                break;

            case BallMoviments.DOWN30RIGHT:
                xDirection = Mathf.Sqrt(3) / 2;
                yDirection = -1f / 2;
                break;

            case BallMoviments.DOWN45RIGHT:
                xDirection = 1;
                yDirection = -1;
                break;

            case BallMoviments.DOWN60RIGHT:
                xDirection = 1f / 2;
                yDirection = -Mathf.Sqrt(3) / 2;
                break;

            case BallMoviments.DOWN30LEFT:
                xDirection = -Mathf.Sqrt(3) / 2;
                yDirection = -1f / 2;
                break;

            case BallMoviments.DOWN45LEFT:
                xDirection = yDirection = -1;
                break;

            case BallMoviments.DOWN60LEFT:
                xDirection = -1f / 2;
                yDirection = -Mathf.Sqrt(3) / 2;
                break;

            default:
                break;
        }
    }

    private void PlayBallSFX(AudioClip audioClip)
    {
        ballSounds.clip = audioClip;
        ballSounds.Play();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            yDirection = -yDirection;
            PlayBallSFX(audioClips[0]);
        }

        if (collision.gameObject.tag == "Player")
        {
            PlayBallSFX(audioClips[1]);
            float hitPoint = GetHitPointHeight(collision.gameObject.transform);
            Debug.Log(hitPoint);

            SetBallDirection(SetBallMoviment(
                hitPoint, 
                collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y,
                collision.gameObject.name));

            //xDirection = -xDirection;
            speed += 0.3f;
        }
    }

    private BallMoviments SetBallMoviment(float hitPoint, float collisionH, string player)
    {
        float collisionHeight = collisionH / 2;

        if(hitPoint >= collisionHeight - 1 * (collisionH / 5))
        {
            return player == "Player1" ? BallMoviments.UP60RIGHT : BallMoviments.UP60LEFT;
        }
        else if(hitPoint >= collisionHeight - 2 * (collisionH / 5))
        {
            return player == "Player1" ? BallMoviments.UP30RIGHT : BallMoviments.UP30LEFT;
        }
        else if(hitPoint >= collisionHeight - 3 * (collisionH/ 5))
        {
            return player == "Player1" ? BallMoviments.STRAIGHTRIGHT : BallMoviments.STRAIGHTLEFT;
        }
        else if (hitPoint >= collisionHeight - 4 * (collisionH / 5))
        {
            return player == "Player1" ? BallMoviments.DOWN30RIGHT : BallMoviments.DOWN30LEFT;
        }
        else if (hitPoint < collisionHeight - 4 * (collisionH / 5))
        {
            return player == "Player1" ? BallMoviments.DOWN60RIGHT : BallMoviments.DOWN60LEFT;
        }

        return player == "Player1" ? BallMoviments.STRAIGHTRIGHT : BallMoviments.STRAIGHTLEFT;
    }

    private float GetHitPointHeight(Transform gameObj)
    {
        float hitPoint, yDistance;

        yDistance = this.transform.position.y - gameObj.position.y;

        hitPoint  = (yDistance * 2) / gameObj.GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.Log(gameObj.GetComponent<SpriteRenderer>().bounds.size.y);

        return hitPoint;
    }
}
