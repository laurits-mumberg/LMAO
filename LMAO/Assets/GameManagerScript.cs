using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public static GameObject LocalPlayerInstance;

    // Use this for initialization
    void Start () {

        PhotonNetwork.JoinRandomRoom();

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
        SpawnPLayer();

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SpawnPLayer()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0), Quaternion.identity, 0);
    }

	// Update is called once per frame
	void Update () {
	}
}
