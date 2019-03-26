using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    //Player variabler
    public GameObject playerPrefab;
    public static GameObject LocalPlayerInstance;
    private GameObject[] allBalls;
    public int playersLeft;

    //Obstacle variabler
    public GameObject[] Level1Obstacles;
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

    //Zone variabler
    public GameObject zoneObj;
    public bool zoneIsActive = false;
    public float zoneGrowSpeed;
    public float zoneSpdChangePerSec;
    private float timeSinceSpdChange;


    // Use this for initialization
    void Start () {
        PhotonNetwork.JoinRandomRoom();

        Level1Obstacles = Resources.LoadAll("Obstacles/Level1", typeof(GameObject)).Cast<GameObject>().ToArray();

        
    }

    void Update()
    {
        CheckForWin();
        Runtimer();
        IncreaseZoneSpped();
    }

    private void IncreaseZoneSpped()
    {
        if (!zoneIsActive)
        {
            return;
        }

        timeSinceSpdChange += Time.deltaTime;

        if (timeSinceSpdChange >= 1)
        {
            timeSinceSpdChange = 0.0f;
            zoneGrowSpeed += zoneSpdChangePerSec;

        }
    }

    private void FixedUpdate()
    {
        MoveZone();
    }

    private void CheckForWin()
    {
        if (!zoneIsActive)
        {
            return;
        }

        //Skift 0 til 1 her, når at spillet rent faktisk skal fungere
        if(playersLeft == 0)
        {
            print("Winner");
            zoneIsActive = false;
            //Gør et eller andet winner, fis
        }
    }

    private void MoveZone()
    {
        if (!zoneIsActive)
        {
            return;
        }

        zoneObj.transform.localScale += new Vector3(0, 0.005f * zoneGrowSpeed, 0);
        zoneObj.transform.position += new Vector3(0, 0.0025f * zoneGrowSpeed, 0);

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



    public IEnumerator SpawnObstacle()
    {

        //Sikrer at baner ikke bliver lavet to gange.
        if (!PhotonNetwork.IsMasterClient)
        {
            obstCurrentCount++;
            yield break;

        }

        //Sikrer at baner ikke bliver spawnet oven i hinanden.
        while ((obstCurrentCount == AllObstacles.Count) == false)
        {
            
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

        obstCurrentCount++;
        string obstName = Level1Obstacles[Random.Range(0, Level1Obstacles.Length)].name;
        PhotonNetwork.InstantiateSceneObject("Obstacles/Level1/" + obstName, newPos, Quaternion.identity, 0);
        
    }

    private void Runtimer()
    {
        if (timerRunning)
        {
            timeLeft = startTime - PhotonNetwork.Time;

            if (timeLeft <= 0)
            {
                //Start
                allBalls = GameObject.FindGameObjectsWithTag("Player");
                playersLeft = PhotonNetwork.CurrentRoom.PlayerCount;
                foreach (GameObject ball in allBalls)
                {
                    ball.GetComponent<BallControl>().canMove = true;
                    timerTextObj.SetActive(false);

                    //Gør så at folk ikke kan joine. Alternativt kan der tilføjes et if-statement ved 5 sekunder tilbage:
                    //PhotonNetwork.room.open = false;
                }
            }

            if (timeLeft <= -5)
            {
                //Starter zonen og stopper timeren
                timerRunning = false;
                zoneIsActive = true;

            }
            timerTextObj.GetComponent<Text>().text = "Starting in: " + (int)timeLeft + " seconds";

        }

    }

}
