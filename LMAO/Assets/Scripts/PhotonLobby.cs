using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonLobby : MonoBehaviourPunCallbacks {

    public static PhotonLobby lobby;
    public BallLook.BallColor colorOfPlayerBall;

    public GameObject connectButton;
    public GameObject cancenButton;

    // Use this for initialization
    void Start() {
        PhotonNetwork.ConnectUsingSettings();
        print("Prøver at connecte");
    }

    public override void OnConnectedToMaster()
    {
        print("Player connected to master");
        connectButton.SetActive(true);
    }

    public void JoinRandomGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
