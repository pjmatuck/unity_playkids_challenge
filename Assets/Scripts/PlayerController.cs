using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isArtificialIntelligence;

    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePaddle(Input.GetAxisRaw("Vertical"));
    }

    private void MovePaddle(float input)
    {
        transform.Translate(new Vector3(0.0f, speed * Time.deltaTime * input));
    }
}
