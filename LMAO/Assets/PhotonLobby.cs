using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks {

    public static PhotonLobby lobby;

    public GameObject connectButton;
    public GameObject cancenButton;

    private void Awake()
    {
        lobby = this;
    }

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
        PhotonNetwork.JoinRandomRoom();
        connectButton.SetActive(false);
        cancenButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Failed to join random room, creating room");
        CreateRoom();

    }

    void CreateRoom()
    {
        //Laver rummet
        int randomRoomNumber = Random.Range(1, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Laver rummet igen. Denne fejl kan også ske, hvis at navnet allerede er taget
        print("Room create failed. Trying again");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        print("Joined room with " + PhotonNetwork.CountOfPlayersInRooms + " players");
        cancenButton.SetActive(false);

    }

    public void CanelJoin()
    {
        cancenButton.SetActive(false);
        connectButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    } 

    // Update is called once per frame
    void Update () {
		
	}
}
