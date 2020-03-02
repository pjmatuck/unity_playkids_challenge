using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public int speed, xDirection, yDirection, xAngle, yAngle;

    // Start is called before the first frame update
    void Start()
    {
        speed = xDirection = yDirection = xAngle = yAngle = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(xDirection * xAngle, yDirection * yAngle) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
            yAngle = -yAngle;

        if (collision.gameObject.tag == "Player")
        {
            xAngle = -xAngle;
            speed++;
        }
    }
}
