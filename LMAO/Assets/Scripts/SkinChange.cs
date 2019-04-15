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

    public BallLook.BallTrail yourTrailColor;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private TrailRenderer tr;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tr = GetComponentInChildren<TrailRenderer>();
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
            default:
                break;
        }
        animator.SetInteger("ColorChange", 0);
    }

    public void SetTrailColor()
    {
        int trail = animator.GetInteger("TrailChange");
        print(trail + " trail");
        switch (trail)
        {
            case 1:
                tr.startColor = Color.yellow;
                tr.endColor = Color.red;
                yourTrailColor = BallLook.BallTrail.Fire;
                prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                break;
            case 2:
                tr.startColor = Color.blue;
                tr.endColor = Color.white;
                yourTrailColor = BallLook.BallTrail.Water;
                prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                break;
        }
        animator.SetInteger("TrailChange", 0);
    }


    //Slet senere
    public void ChangeToNextColor(int nextColorInt)
    {
        animator.SetInteger("ColorChange", nextColorInt);
    }

    public void ChangeToNextTrail(int nextTrailInt)
    {
        animator.SetInteger("TrailChange", nextTrailInt);
    }

    public void SubmitPlayerInfo()
    {
        playerInfoTable.Add("ballcolor", yourBallColor);
        playerInfoTable.Add("ballTrail", yourTrailColor);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInfoTable);
    }
}
