using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviourPunCallbacks
{

    public Transform target;
    public bool followPlayer = true;
    public float smoothTime = 0.3f;
    public float xOffset;
    public float yOffset;

    private Vector3 velocity = Vector3.zero;
    private GameObject ballToFollow;

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (followPlayer)
        {
            FollowPLayer();
        }
    }

    private void FollowPLayer()
    {
        if (target == null)
        {
            GameObject[] AllCurrentBalls;
            AllCurrentBalls = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject ball in AllCurrentBalls)
            {
                if (ball.GetComponent<PhotonView>().IsMine)
                {
                    target = ball.transform;
                }
            }
        }
        else
        {

            Vector3 goalPos = target.position;

            //Sikrer at den ikke flyver ned i banen
            goalPos.z = transform.position.z;

            //Tilføjer offset
            goalPos.x += xOffset;
            goalPos.y += yOffset;

            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        }

    }
}
