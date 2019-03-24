using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviourPun {

    public bool canMove;
    private GameObject gameManagerObj;

    public float power;
    public float maxSpeed;
    public float breakSpeed;

    public Vector2 vectorToShoot;

    private Rigidbody2D rb2d;

    private bool isMoving = false;
    public bool isShooting = false;

   
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        canMove = false;
    }

    private void Update()
    {
        if (canMove)
        {
            BallShooting();
        }
    }


    void FixedUpdate()
    {
        //Slower bare ned hele tiden
        rb2d.velocity = rb2d.velocity * breakSpeed;
    }

    private void BallShooting()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0) && !isMoving)
            {
                isShooting = true;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                vectorToShoot = (gameObject.transform.position - mousePos) * power;
                vectorToShoot = Vector2.ClampMagnitude(vectorToShoot, maxSpeed);
            }

            if (!isMoving && Input.GetMouseButtonUp(0) && isShooting)
            {

                if (vectorToShoot.magnitude < 1)
                {
                    //Cancel shit
                    isShooting = false;

                }
                else
                {
                    rb2d.AddForce(vectorToShoot, ForceMode2D.Impulse);
                    isMoving = true;
                    isShooting = false;
                }
            }
        }


        //Checker om man må skyde igen
        if (rb2d.velocity.magnitude < 0.1f)
        {
            rb2d.velocity = new Vector2(0, 0);
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

}
