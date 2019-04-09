using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb2d;
    private bool isLeft = true;
    public float posChangeX;
    public float posChangeY;

    private Vector3 originalPos;
    private Vector3 targetPos;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalPos = transform.position;

        if (Random.value < .5)
        {
            MoveThisWall();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        

        if (isMoving)
        {
            if((transform.position - targetPos).magnitude < 0.15)
            {
                rb2d.MovePosition(targetPos);
                isMoving = false;
                return;
            }

            Vector3 vectorToTarget = (targetPos - transform.position).normalized;
            rb2d.MovePosition(transform.position + vectorToTarget * moveSpeed * 0.01f);
        }
    }

    public void MoveThisWall()
    {
        if (isLeft)
        {
            targetPos = originalPos + new Vector3(posChangeX, posChangeY);
            isLeft = false;
        }
        else
        {
            targetPos = originalPos;
            isLeft = true;
        }

        isMoving = true;
    }
}
