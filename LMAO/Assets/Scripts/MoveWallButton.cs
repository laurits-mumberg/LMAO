using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWallButton : MonoBehaviour
{

    public GameObject wallToMove;
    public bool delayActive = false;
    public float delayTime;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delayActive)
        {
            if(Time.time - startTime > delayTime)
            {
                delayActive = false;
                GetComponent<Animator>().Play("buttonUp");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (delayActive)
        {
            return;
        }

        if (collision.transform.tag == "Player")
        {
            GetComponent<Animator>().Play("buttonDown");
            wallToMove.GetComponent<MoveWall>().MoveThisWall();
            delayActive = true;
            startTime = Time.time;
        }
    }
}
