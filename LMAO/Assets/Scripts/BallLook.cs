using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLook : MonoBehaviourPunCallbacks
{

    public enum BallColor {White, Red, Green, Blue}
    public enum BallTrail {Fire, Water, Cool, Lolol, haha}
    public int emojiNumber;

    //[0]= normalMaterial, [1]= lololMaterial, [2]= hahaMaterial
    public Material[] curMaterial;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    public TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        SetballColor();
        SetTrailColor();
        SetEmoji();
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
        BallTrail thisBallsTrail = (BallTrail)photonView.Owner.CustomProperties["ballTrail"];
        switch (thisBallsTrail)
        {
            case BallTrail.Fire:
                TrailSettings(Color.yellow, Color.red, curMaterial[0], 0.25f, 0.0f);
                break;
            case BallTrail.Water:
                TrailSettings(Color.blue, Color.cyan, curMaterial[0], 0.25f, 0.0f);
                break;
            case BallTrail.Lolol:
                TrailSettings(Color.white, Color.white, curMaterial[1], 0.25f, 0.25f);
                break;
            case BallTrail.haha:
                TrailSettings(Color.white, Color.white, curMaterial[2], 0.25f, 0.25f);
                break;
            case BallTrail.Cool:
                TrailSettings(Color.green,Color.yellow,curMaterial[0],0.25f,0.0f);
                break;

        }
    }

    private void TrailSettings(Color startCl, Color endCl, Material mat, float startWdt, float endWdt)
    {
        tr.startColor = startCl;
        tr.endColor = endCl;
        tr.material = mat;
        tr.startWidth = startWdt;
        tr.endWidth = endWdt;
    }

    public void SetEmoji()
    {
        emojiNumber = (int)photonView.Owner.CustomProperties["ballEmoji"];
    }

}
