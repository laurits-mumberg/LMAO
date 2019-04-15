using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLook : MonoBehaviourPunCallbacks
{

    public enum BallColor {White, Red, Green, Blue}
    public enum BallTrail {Fire, Water, Lolol, haha}

    public Material normalMaterial;
    public Material lololMaterial;
    public Material hahaMaterial;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    public TrailRenderer tr;


    // Start is called before the first frame update
    void Start()
    {
        SetballColor();
        SetTrailColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetballColor()
    {
        BallColor thisBallsColor = (BallColor)photonView.Owner.CustomProperties["ballcolor"];
        print(thisBallsColor);
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
        BallTrail thisBallsTrail = (BallTrail)photonView.Owner.CustomProperties["ballTrail"];
        print(thisBallsTrail);
        switch (thisBallsTrail)
        {
            case BallTrail.Fire:
                tr.startColor = Color.yellow;
                tr.endColor = Color.red;
                tr.material = normalMaterial;
                tr.startWidth = 0.25f;
                tr.endWidth = 0.0f;
                break;
            case BallTrail.Water:
                tr.startColor = Color.blue;
                tr.endColor = Color.white;
                tr.material = normalMaterial;
                tr.startWidth = 0.25f;
                tr.endWidth = 0.0f;
                break;
            case BallTrail.Lolol:
                tr.startColor = Color.white;
                tr.endColor = Color.white;
                tr.material = lololMaterial;
                tr.startWidth = 0.25f;
                tr.endWidth = 0.25f;
                break;
            case BallTrail.haha:
                tr.startColor = Color.white;
                tr.endColor = Color.white;
                tr.material = hahaMaterial;
                tr.startWidth = 0.25f;
                tr.endWidth = 0.25f;
                break;
        }
    }
}
