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
    private Hashtable playerInfoTable = new Hashtable();
    Animator animator;

    public BallLook.BallColor yourBallColor;
    public BallLook.BallTrail yourTrailColor;
    public BallLook.BallEmoji yourEmoji;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private TrailRenderer tr;

    public Material normalMaterial;
    public Material lololMaterial;
    public Material hahaMaterial;

    public int curEmojiNumber;
    public GameObject[] curEmoji;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tr = GetComponentInChildren<TrailRenderer>();
    }

    public void SetBallColor()
    {
        int color = animator.GetInteger("ColorChange");

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
        switch (trail)
        {
            case 1:
                tr.startColor = Color.yellow;
                tr.endColor = Color.red;
                yourTrailColor = BallLook.BallTrail.Fire;
                prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                tr.material = normalMaterial;
                tr.startWidth = 0.5f;
                tr.endWidth = 0.0f;
                break;
            case 2:
                tr.startColor = Color.blue;
                tr.endColor = Color.white;
                yourTrailColor = BallLook.BallTrail.Water;
                prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                tr.material = normalMaterial;
                tr.startWidth = 0.5f;
                tr.endWidth = 0.0f;
                break;
            case 3:
                tr.startColor = Color.white;
                tr.endColor = Color.white;
                yourTrailColor = BallLook.BallTrail.Lolol;
                prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                tr.material = lololMaterial;
                tr.startWidth = 0.5f;
                tr.endWidth = 0.5f;
                break;
            case 4:
                tr.startColor = Color.white;
                tr.endColor = Color.white;
                yourTrailColor = BallLook.BallTrail.haha;
                prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                tr.material = hahaMaterial;
                tr.startWidth = 0.5f;
                tr.endWidth = 0.5f;
                break;

        }
        animator.SetInteger("TrailChange", 0);

    }

    public void SetEmoji()
    {

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

    public void ChangeToNextEmoji(int nextEmojiInt)
    {

    }

    public void SubmitPlayerInfo()
    {
        playerInfoTable.Add("ballcolor", yourBallColor);
        playerInfoTable.Add("ballTrail", yourTrailColor);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInfoTable);
    }
}
