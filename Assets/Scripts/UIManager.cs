using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Start text control
    public Text startText;
    public float startTextBlinkInterval;
    private float startTextBlinkTimePassed;
    private bool startTextIsOn;

    //Score text control
    public Text p1Score;
    public Text p2Score;
    public float scoreBlinkingTime;
    private Color scoreOriginalColor;

    //UI Containers
    public GameObject scoreContainer;
    public GameObject playersContainer;
    public GameObject midFieldLine;
    public GameObject righGoal, leftGoal;
    public BallSpawnerBehavior ballSpawner;
    public GameObject pressStartPanel;
    public GameObject endGamePanel;

    public Text winnerText;

    //SFX | Win and Lose
    public AudioClip[] audioClips;
    AudioSource audioSource;

    // Ball reference
    GameObject ball;

    void Start()
    {
        startTextBlinkTimePassed = 0;

        scoreOriginalColor = p1Score.color;

        startTextIsOn = startText.IsActive();

        ballSpawner.LaunchBallRandomly();

        ShowPressStartScreen(true);

        audioSource = GetComponent<AudioSource>();
    }

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

        if (score > 0) StartCoroutine(BlinkScore(player));
    }

    //Enable-Disable gameplay elements: player, score and midfield line.
    private void ShowPlayElements(bool toShow)
    {
        scoreContainer.SetActive(toShow);
        playersContainer.SetActive(toShow);
        midFieldLine.SetActive(toShow);

        righGoal.GetComponent<Collider2D>().isTrigger = toShow;
        leftGoal.GetComponent<Collider2D>().isTrigger = toShow;
    }

    private IEnumerator BlinkScore(int player)
    { 
        if (player == 1) p1Score.color = Color.white;
        if (player == 2) p2Score.color = Color.white;

        //scoreTextBlinkTimePassed += Time.deltaTime;

        //if(scoreTextBlinkTimePassed > scoreBlinkingTime)
        //{

        yield return new WaitForSeconds(scoreBlinkingTime);

        if (player == 1) p1Score.color = scoreOriginalColor;
        if (player == 2) p2Score.color = scoreOriginalColor;

            //scoreTextBlinkTimePassed = 0;
        //}

        yield return null;
    }
}
