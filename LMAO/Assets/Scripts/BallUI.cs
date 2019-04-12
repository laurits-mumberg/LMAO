using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject gameManagerObj;

    //Zone ting
    private GameObject lengthToMarkerUI;
    private GameObject zoneMarkerObj;
    public GameObject markerUpArrow;
    public GameObject markerDownArrow;

    void Start()
    {

        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        CanvasSetup();


    }

    void CanvasSetup()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        lengthToMarkerUI = canvas.transform.Find("MarkerText").gameObject;
        lengthToMarkerUI.GetComponent<Text>().enabled = false;
        markerUpArrow = lengthToMarkerUI.transform.Find("UpArrow").gameObject;
        markerDownArrow = lengthToMarkerUI.transform.Find("DownArrow").gameObject;
    }

    void Update()
    {

        UpdateMarkerUI();


    }

    void UpdateMarkerUI()
    {
        if(gameManagerObj.GetComponent<GameManagerScript>().zoneIsActive == false)
        {
            return;
        }

        else if(zoneMarkerObj == null)
        {
            zoneMarkerObj = GameObject.FindGameObjectWithTag("zoneMarker");
            lengthToMarkerUI.GetComponent<Text>().enabled = true;
        }

        int lengthToMarker = (int)(zoneMarkerObj.transform.position.y - transform.position.y);
        
        if(lengthToMarker <= 0)
        {
            //Spilleren er foran markeren
            lengthToMarkerUI.GetComponent<Text>().text = " Next zone is \n" + Mathf.Abs(lengthToMarker) + "\n meters behind you";
            markerDownArrow.SetActive(true);
            markerUpArrow.SetActive(false);
        }

        if (lengthToMarker > 0)
        {
            //Spilleren er bag ved markeren
            lengthToMarkerUI.GetComponent<Text>().text = " Next zone is \n" + lengthToMarker + "\n meters away";
            markerDownArrow.SetActive(false);
            markerUpArrow.SetActive(true);
        }
    }
}
