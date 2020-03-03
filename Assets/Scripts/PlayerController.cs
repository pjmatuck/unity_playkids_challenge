using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    bool onColision = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePaddle(Input.GetAxisRaw("Vertical"));
    }

    private void MovePaddle(float input)
    {
        transform.Translate(new Vector3(0.0f, speed * Time.deltaTime * input));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Debug.Log("Collision -> Border");
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision -> Player");
        }
    }
}
