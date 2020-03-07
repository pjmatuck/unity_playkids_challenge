using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatiosHandler : MonoBehaviour
{
    private const float aspectRatioReference = 16f / 9f;

    float newAspectRatio;
    float adjustFactor;

    public GameObject[] paddles;
    public GameObject[] goals;
    public GameObject ball;

    private void Awake()
    {
        newAspectRatio = (float)Screen.width / (float)Screen.height;
        Debug.Log("Screen -> " + "Width: " + Screen.width + " Height: " + Screen.height + " | Ratio: " + newAspectRatio);

        if (decimal.Round(Convert.ToDecimal(newAspectRatio),2) != decimal.Round(Convert.ToDecimal(aspectRatioReference), 2))
        {
            adjustFactor = newAspectRatio / aspectRatioReference;

            foreach (var item in paddles)
            {
                AdjustXPosition(item.transform);
                AdjustScale(item.transform);
            }

            foreach (var item in goals)
            {
                AdjustXPosition(item.transform);
            }

            AdjustScale(ball.transform);
        }
    }

    private void AdjustXPosition(Transform transform)
    {
        transform.position = new Vector3(transform.position.x * adjustFactor, 0);
    }

    private void AdjustScale(Transform transform)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y) * adjustFactor;
    }
}
