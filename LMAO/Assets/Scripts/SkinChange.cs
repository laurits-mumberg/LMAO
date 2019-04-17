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

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private TrailRenderer tr;

    public Material normalMaterial;
    public Material lololMaterial;
    public Material hahaMaterial;

    public int emojiNumber;

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
                TrailSettings(Color.yellow, Color.red, BallLook.BallTrail.Fire, normalMaterial, 0.5f, 0.0f);
                break;
            case 2:
                TrailSettings(Color.blue, Color.cyan, BallLook.BallTrail.Water, normalMaterial, 0.5f, 0.0f);
                break;
            case 3:
                TrailSettings(Color.white, Color.white, BallLook.BallTrail.Lolol, lololMaterial, 0.5f, 0.5f);
                break;
            case 4:
                TrailSettings(Color.white, Color.white, BallLook.BallTrail.haha, hahaMaterial, 0.5f, 0.5f);
                break;
            case 5:
                TrailSettings(Color.green,Color.yellow,BallLook.BallTrail.Cool, normalMaterial, 0.5f, 0.0f);
                break;

        }
        animator.SetInteger("TrailChange", 0);

    }

    private void TrailSettings(Color startCl, Color endCl, BallLook.BallTrail trailCl, Material mat, float startWdt, float endWdt)
    {
        tr.startColor = startCl;
        tr.endColor = endCl;
        yourTrailColor = trailCl;
        prewiewBall.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
        tr.material = mat;
        tr.startWidth = startWdt;
        tr.endWidth = endWdt;
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

    public void ChangeToNextEmoji(int curEmoji)
    {
        emojiNumber = curEmoji;
    }

    public void SubmitPlayerInfo()
    {
        playerInfoTable.Add("ballcolor", yourBallColor);
        playerInfoTable.Add("ballTrail", yourTrailColor);
        playerInfoTable.Add("ballEmoji", emojiNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInfoTable);
    }
}
