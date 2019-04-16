using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoneControl : MonoBehaviour
{
    public GameObject gameManger;
    public GameObject markerObject;
    public float timeBetweenZoneMoves = 1;
    public float timeBetweenMarkerMoves = 1;
    public float standardMoveLength;
    public float lengthIncrease;
    public float zoneMoveSpeed;
    public float markerMoveSpeed;

    public float timeLeftBeforeZoneMove;
    private float timeLeftBeforeMarkerMove;
    public bool zoneReachedPos;
    public bool markerReachedPos = false;
    public Vector3 nextPos;
    public Vector3 lastPos;
    public float zoneOffset;
    private int timesMoved = 0;
    
    


    // Start is called before the first frame update
    void Start()
    {
        nextPos = markerObject.transform.position;
        lastPos = nextPos;
        zoneOffset = transform.position.y + (transform.position.y - markerObject.transform.position.y);
        setNextMarker();
        timeLeftBeforeZoneMove = timeBetweenZoneMoves;
    }

    // Update is called once per frame
    void Update()
    {
        MoveZone();
    }


    void MoveZone()
    {
        //Om zonen overhovedet skal bevæge sig
        if (!gameManger.GetComponent<GameManagerScript>().zoneIsActive)
        {
            return;
        }
        markerObject.SetActive(true);

        if (!markerReachedPos)
        {
            markerObject.transform.position += new Vector3(0, (markerMoveSpeed + timesMoved * lengthIncrease) * Time.deltaTime * 0.5f);

            if (markerObject.transform.position.y >= nextPos.y)
            {
                markerReachedPos = true;
                timeLeftBeforeZoneMove = timeBetweenZoneMoves;
                timeLeftBeforeMarkerMove = timeBetweenMarkerMoves;
            }
        }
        else
        {

            timeLeftBeforeZoneMove -= Time.deltaTime;
            if((timeLeftBeforeZoneMove <= 0) == false)
            {
                //Der er stadigvæk tid tilbage
                return;
            }

            if (transform.localScale.y + zoneOffset <= markerObject.transform.position.y)
            {
                transform.localScale += new Vector3(0, (zoneMoveSpeed + timesMoved * lengthIncrease) * Time.deltaTime);
                transform.position += new Vector3(0, (zoneMoveSpeed + timesMoved * lengthIncrease) * Time.deltaTime * 0.5f);
            }
            else
            {
                timeLeftBeforeMarkerMove -= Time.deltaTime;
                if ((timeLeftBeforeMarkerMove <= 0) == false)
                {
                    //Der er stadigvæk tid tilbage
                    return;
                }

                zoneReachedPos = true;
                timesMoved++;
                setNextMarker();

            }

        }

    }


    void setNextMarker()
    {
        lastPos = nextPos;
        nextPos = new Vector3(nextPos.x, nextPos.y + standardMoveLength + lengthIncrease * timesMoved);
        markerReachedPos = false;
    }
}
