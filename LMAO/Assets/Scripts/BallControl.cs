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
    public float minSpeed = 0.5f;

    public Vector2 vectorToShoot;

    private Rigidbody2D rb2d;
    private bool isMoving = false;
    public bool isShooting = false;

    //stats
    public float health = 100;
    public float dmgperSecInZone;
    private float timeSinceDmgTaken = 0;

   
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

        if (health <= 0)
        {
            photonView.RPC("PlayerDie", RpcTarget.All);
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
        if (rb2d.velocity.magnitude < minSpeed)
        {
            rb2d.velocity = new Vector2(0, 0);
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (collision.tag == "zone")
        {
            timeSinceDmgTaken += Time.deltaTime;

            if (timeSinceDmgTaken >= 1)
            {
                //Tag damage
                health -= dmgperSecInZone;
                timeSinceDmgTaken = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (collision.tag == "zone")
            {
                timeSinceDmgTaken = 0;
            }
        }
    }
    [PunRPC]
    void PlayerDie()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        gameManagerObj.GetComponent<GameManagerScript>().playersLeft--;
    }

}
