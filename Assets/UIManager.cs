using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public float startTextBlinkInterval;
    public Text startText;
    private float startTextBlinkTimePassed;
    private bool startTextIsOn;

    public GameObject scoreContainer;
    public GameObject playersContainer;
    public GameObject midFieldLine;
    public GameObject righGoal, leftGoal;
    public BallSpawnerBehavior ballSpawner;

    public GameObject pressStartPanel;
    public GameObject endGamePanel;
    public Text p1Score;
    public Text p2Score;
    public Text winnerText;

    public AudioClip[] audioClips;
    AudioSource audioSource;

    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        startTextBlinkTimePassed = 0;
        startTextIsOn = startText.IsActive();

        ballSpawner.LaunchBallRandomly();

        ShowPressStartScreen(true);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BlinkStartText();
    }

    private void BlinkStartText()
    {
        startTextBlinkTimePassed += Time.deltaTime;

        if(startTextBlinkTimePassed > startTextBlinkInterval)
        {
            startTextIsOn = !startTextIsOn;
            startTextBlinkTimePassed = 0;
        }

        startText.enabled = (startTextIsOn ? true : false);
    }

    public void ShowPressStartScreen(bool enabled)
    {
        pressStartPanel.SetActive(enabled);

        ShowPlayElements(!enabled);
    }

    public void ShowEndGameScreen(bool enabled, int? winnerPlayer = null)
    {
        if (winnerPlayer != null)
        {
            winnerText.text = "Player " + winnerPlayer + " wins!";
            audioSource.clip = (winnerPlayer == 1 ? audioClips[0] : audioClips[1]);
            audioSource.Play();
        }

        endGamePanel.SetActive(enabled);

        ShowPlayElements(!enabled);
    }

    public void UpdateGameScore(int player, int score)
    {
        if(player == 1)
        {
            p1Score.text = score.ToString();
        } else
        {
            p2Score.text = score.ToString();
        }
    }

    private void ShowPlayElements(bool toSHow)
    {
        scoreContainer.SetActive(toSHow);
        playersContainer.SetActive(toSHow);
        midFieldLine.SetActive(toSHow);

        righGoal.GetComponent<Collider2D>().isTrigger = toSHow;
        leftGoal.GetComponent<Collider2D>().isTrigger = toSHow;
    }
}
