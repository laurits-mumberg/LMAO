using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoScript : MonoBehaviourPunCallbacks
{
    public static PlayerInfoScript instance;
    private Hashtable playerInfoTable = new Hashtable();
    public GameObject prewiewBall;
    public BallLook.BallColor yourBallColor;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBallColor(string color)
    {
        switch (color)
        {
            case "white":
                yourBallColor = BallLook.BallColor.White;
                prewiewBall.GetComponent<Renderer>().material.color = Color.white;
                break;
            case "red":
                yourBallColor = BallLook.BallColor.Red;
                prewiewBall.GetComponent<Renderer>().material.color = Color.red;
                break;
            case "green":
                yourBallColor = BallLook.BallColor.Green;
                prewiewBall.GetComponent<Renderer>().material.color = Color.green;
                break;
            case "blue":
                yourBallColor = BallLook.BallColor.Blue;
                prewiewBall.GetComponent<Renderer>().material.color = Color.blue;
                break;
            default:
                yourBallColor = BallLook.BallColor.White;
                prewiewBall.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }

    public void SubmitPlayerInfo()
    {
        playerInfoTable.Add("ballcolor", yourBallColor);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInfoTable);
    }
}
