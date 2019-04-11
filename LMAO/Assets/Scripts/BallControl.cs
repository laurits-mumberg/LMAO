using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviourPun {

    public bool canMove;

    //Objekter i scenen
    private GameObject gameManagerObj;
    private GameObject canvas;
    private GameObject healthTextObj;

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

    //Animationsvariabler
    private bool hasJumpedIn;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("DisableRenderer", RpcTarget.All);
        }

    }

    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        healthTextObj = canvas.transform.Find("HealthText").gameObject;
        canMove = false;
    }

    private void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }

        if (canMove)
        {
            BallShooting();
        }

        if (health <= 0)
        {
            photonView.RPC("PlayerDie", RpcTarget.All);
        }



        healthTextObj.GetComponent<Text>().text = "Health: " + health;


        if (!hasJumpedIn)
        {
            if (!Camera.main.GetComponent<CameraFollowPlayer>().cameraReady)
            {
                return;
            }

            //Gør så at alle animationer ikke kører, når at man joiner et rum.
            if (photonView.IsMine)
            {
                photonView.RPC("SpawnAnim", RpcTarget.All);
                hasJumpedIn = true;
            }


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
                    //isMoving = true;
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
            isShooting = false;
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

    [PunRPC]
    void SpawnAnim()
    {
        GetComponent<Animator>().Play("BallSpawnAnim");
        GetComponent<Renderer>().enabled = true;
    }

    [PunRPC]
    void DisableRenderer()
    {
        GetComponent<Renderer>().enabled = false;
    }


    private void OnDestroy()
    {
        if (!gameManagerObj.GetComponent<GameManagerScript>().zoneIsActive)
        {
            return;
        }
        gameManagerObj.GetComponent<GameManagerScript>().playersLeft--;

        if (photonView.IsMine)
        {
            GameObject leaveButton = canvas.transform.Find("ButtonLeave").gameObject;
            leaveButton.SetActive(true);
        }

    }

    public void RemoveAnim()
    {
        GetComponent<Animator>().enabled = false;

        if (photonView.IsMine)
        {
            Camera.main.GetComponent<CameraFollowPlayer>().followPlayer = true;
        }
    }

}
