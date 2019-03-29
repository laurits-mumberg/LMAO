using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWall : MonoBehaviour
{

    public float rotateSpeed;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(Random.value < 0.5f)
        {
            rotateSpeed = -rotateSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(new Vector3(0,0,1) * rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float newAngle = rb2d.rotation + 1 * rotateSpeed * Time.deltaTime;
        rb2d.MoveRotation(newAngle);
    }
}
