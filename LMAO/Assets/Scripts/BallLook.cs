using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLook : MonoBehaviourPunCallbacks
{

    public enum BallColor {White, Red, Green, Blue}
    public enum BallTrail {Fire, Water}

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private TrailRenderer tr;


    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.GetComponentInChildren<TrailRenderer>();
        SetballColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetballColor()
    {
        BallColor thisBallsColor = (BallColor)photonView.Owner.CustomProperties["ballcolor"];
        switch (thisBallsColor)
        {
            case BallColor.Red:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case BallColor.Green:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case BallColor.Blue:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case BallColor.White:
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            default:
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
        }

    }

    public void SetTrailColor()
    {
        BallTrail thisBallsTrail = (BallTrail)photonView.Owner.CustomProperties["ballcolor"];
        switch (thisBallsTrail)
        {
            case BallTrail.Fire:
                tr.startColor = Color.yellow;
                tr.endColor = Color.red;
                gameObject.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                break;
            case BallTrail.Water:
                tr.startColor = Color.blue;
                tr.endColor = Color.white;
                gameObject.GetComponentInChildren<TrailRenderer>().colorGradient = tr.colorGradient;
                break;
        }
    }
}
