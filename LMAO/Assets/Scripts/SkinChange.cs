using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkinChange : MonoBehaviourPunCallbacks
{
    public GameObject prewiewBall;
    public BallLook.BallColor yourBallColor;
    private Hashtable playerInfoTable = new Hashtable();
    Animator animator;

    //Slet senere
    private int currentColorInt = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBallColor()
    {
        int color = animator.GetInteger("ColorChange");
        print(color);
        switch (color)
        {
            case 1:
                yourBallColor = BallLook.BallColor.White;
                prewiewBall.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2:
                yourBallColor = BallLook.BallColor.Red;
                prewiewBall.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 3:
                yourBallColor = BallLook.BallColor.Green;
                prewiewBall.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 4:
                yourBallColor = BallLook.BallColor.Blue;
                prewiewBall.GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
        animator.SetInteger("ColorChange", 0);
    }


    //Slet senere
    public void ChangeToNextColor()
    {
        int nextColorInt = (currentColorInt + 1) % 4;
        animator.SetInteger("ColorChange", nextColorInt);
        currentColorInt = nextColorInt;
    }

    public void SubmitPlayerInfo()
    {
        playerInfoTable.Add("ballcolor", yourBallColor);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInfoTable);
    }
}
