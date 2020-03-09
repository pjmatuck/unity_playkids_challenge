using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehavior : MonoBehaviour
{
    [SerializeField]
    private float startSpeed;
    [SerializeField]
    private float increaseSpeedRate;

    public AudioClip[] audioClips;

    private GameManager gameManager;

    public bool IsToRestart { get; set; }
    public int PlayerScorer { get; set; }

    private float xDirection, yDirection;
    private AudioSource ballSounds;

    void Start()
    {
        IsToRestart = false;
        gameManager = GameManager.GetGameManagerInstace();
        increaseSpeedRate = startSpeed;
        ballSounds = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        transform.Translate(new Vector3(xDirection, yDirection) * increaseSpeedRate * Time.deltaTime);
    }

    /*
     * This method is based on Trigonometric Table
     * sin30: 0.5f, sin60: sqrt(3)/2, sin90: 1
     * cos30: sqrt(3)/2, cos60: 0.5f, cos90: 0
     * tan45: 1
     */
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
        //Invert y direction if hits the top or bottom walls
        if (collision.gameObject.tag == "Border")
        {
            yDirection = -yDirection;
            PlayBallSFX(audioClips[0]);
        }

        //Every time which hits a player the ball speed is increased
        if (collision.gameObject.tag == "Player")
        {
            PlayBallSFX(audioClips[1]);
            float hitPoint = GetHitPointHeight(collision.gameObject.transform);
            Debug.Log(hitPoint);

            SetBallDirection(SetBallMoviment(
                hitPoint, 
                collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y,
                collision.gameObject.name));

            increaseSpeedRate += 0.3f;
        }

        //It should happen only on Start Screen when the Goals are standard colliders, not triggers.
        if(collision.gameObject.tag == "Goal")
        {
            xDirection = -xDirection;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            GameObject goal = collision.gameObject;

            if (goal.name == "Player1Goal")
                PlayerScorer = 2;
            else
                PlayerScorer = 1;

            IsToRestart = true;
            gameManager.AddPointToPlayer(PlayerScorer);
            PlayBallSFX(audioClips[2]);
        }
    }

    //Define the ball moviment according with where the ball collided on player and which player
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

    //Get the point where the ball hit the player
    private float GetHitPointHeight(Transform gameObj)
    {
        float hitPoint, yDistance;

        yDistance = this.transform.position.y - gameObj.position.y;

        hitPoint  = (yDistance * 2) / gameObj.GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.Log(gameObj.GetComponent<SpriteRenderer>().bounds.size.y);

        return hitPoint;
    }

    public void SetSpeed(float newSpeed)
    {
        this.startSpeed = newSpeed;
    }

    public void RestartSpeed()
    {
        increaseSpeedRate = startSpeed;
    }
}
