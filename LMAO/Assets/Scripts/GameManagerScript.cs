using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    //Player variabler
    public GameObject playerPrefab;
    public static GameObject LocalPlayerInstance;
    private GameObject[] AllBalls;

    //Obstacle Variabler
    public List<GameObject> AllObstacles = new List<GameObject>();
    public int obstSpawnAtStartCount = 3;
    private int obstCurrentCount = 0;

    //Timer variabler
    public GameObject timerTextObj;
    private double startTime;
    public bool timerRunning;
    private double timerDoneTime;
    public double timerTime;
    public double timeLeft;


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
        
        //Sætter timer op:
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            Hashtable roomCustomProperties = new Hashtable();
            startTime = PhotonNetwork.Time + timerTime;
            roomCustomProperties.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        }
        timerRunning = true;

        SpawnPLayer();

        for (int i = 1; i <= obstSpawnAtStartCount; i++)
        {
            StartCoroutine(SpawnObstacle());
        }


    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SpawnPLayer()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0 + ((PhotonNetwork.CurrentRoom.PlayerCount - 1) * 0.5f), 0, 0), Quaternion.identity, 0);
    }

    void Update ()
    {
        Runtimer();
	}

    public IEnumerator SpawnObstacle()
    {

        //Sikrer at baner ikke bliver lavet to gange.
        if (!PhotonNetwork.IsMasterClient)
        {
            obstCurrentCount++;
            yield break;

        }

        while ((obstCurrentCount == AllObstacles.Count) == false)
        {
            print("obstCurrentCount er " + obstCurrentCount + " og AllObstacles.Count er " + AllObstacles.Count);
            yield return new WaitForSeconds(0.1f);
        }

        Vector2 newPos;

        if (AllObstacles.Count == 0)
        {
            newPos = new Vector2(0, 10);
        }
        else
        {
            newPos = new Vector2(AllObstacles[AllObstacles.Count - 1].transform.position.x, AllObstacles[AllObstacles.Count - 1].transform.position.y) + AllObstacles[AllObstacles.Count - 1].GetComponent<Obstacle>().vectorToEnd;
        }

        print("Spawner ny ting");
        obstCurrentCount++;
        PhotonNetwork.Instantiate("Level1Obs/TestObs", newPos, Quaternion.identity, 0);
        
    }

    private void Runtimer()
    {
        if (timerRunning)
        {
            timeLeft = startTime - PhotonNetwork.Time;

            if (timeLeft <= 0)
            {
                //Start spillet
                timerRunning = false;
                AllBalls = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject ball in AllBalls)
                {
                    ball.GetComponent<BallControl>().canMove = true;
                    timerTextObj.SetActive(false);

                    //Gør så at folk ikke kan joine. Alternativt kan der tilføjes et if-statement ved 5 sekunder tilbage:
                    //PhotonNetwork.room.open = false;
                }
            }
            timerTextObj.GetComponent<Text>().text = "Starting in: " + (int)timeLeft + " seconds";

        }

    }
}
