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
    public BallLook.BallColor yourBallColor;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        playerInfoTable.Add("ballcolor", yourBallColor);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerInfoTable);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
