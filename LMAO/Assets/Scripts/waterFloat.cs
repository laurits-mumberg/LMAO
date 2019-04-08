using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFloat : MonoBehaviour
{
    public float xPushForce;
    public float yPushForce;
    public float maxSpeed;
    public float newBreakSpeed = 0.5f;

    private float ballBreakSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballBreakSpeed = collision.gameObject.GetComponent<BallControl>().breakSpeed;
        collision.gameObject.GetComponent<BallControl>().breakSpeed = newBreakSpeed;
        collision.gameObject.GetComponent<BallControl>().canMove = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }

        if (!collision.gameObject.GetComponent<BallControl>().photonView.IsMine)
        {
            return;
        }

        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPushForce, yPushForce));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }

        if (!collision.gameObject.GetComponent<BallControl>().photonView.IsMine)
        {
            return;
        }

        collision.gameObject.GetComponent<BallControl>().canMove = true;
        collision.gameObject.GetComponent<BallControl>().breakSpeed = ballBreakSpeed;
    }

}
