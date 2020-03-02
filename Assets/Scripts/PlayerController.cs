using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    float input;
    bool onColision = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input = Input.GetAxis("Vertical");

        if (!onColision)
            transform.Translate(new Vector3(0.0f, speed * Time.deltaTime * input));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Debug.Log("Collision -> Border");
        }
    }
}
