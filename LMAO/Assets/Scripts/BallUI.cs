using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallUI : MonoBehaviourPun
{
    public GameObject canvas;
    public GameObject gameManagerObj;

    //Zone ting
    private GameObject lengthToMarkerUI;
    private GameObject zoneMarkerObj;
    public GameObject zoneObj;
    public GameObject markerUpArrow;
    public GameObject markerDownArrow;

    public float sliderRange;
    public GameObject zoneInfoObj;
    public Slider zoneSlider;
    public Slider ballSlider;
    public Slider markerSlider;

    void Start()
    {

        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        CanvasSetup();


    }

    void CanvasSetup()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        lengthToMarkerUI = canvas.transform.Find("MarkerText").gameObject;
        lengthToMarkerUI.GetComponent<Text>().enabled = false;
        markerUpArrow = lengthToMarkerUI.transform.Find("UpArrow").gameObject;
        markerDownArrow = lengthToMarkerUI.transform.Find("DownArrow").gameObject;

        zoneInfoObj = canvas.transform.Find("ZoneInfo").gameObject;
        zoneSlider = zoneInfoObj.transform.Find("ZoneSlider").gameObject.GetComponent<Slider>();
        ballSlider = zoneInfoObj.transform.Find("BallSlider").gameObject.GetComponent<Slider>();
        markerSlider = zoneInfoObj.transform.Find("MarkerSlider").gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //UpdateMarkerUI();
        UpdateMarkerUI2();

    }

    void UpdateMarkerUI2()
    {
        if (gameManagerObj.GetComponent<GameManagerScript>().zoneIsActive == false)
        {
            return;
        }


        if (zoneMarkerObj == null)
        {
            zoneMarkerObj = GameObject.FindGameObjectWithTag("zoneMarker");
            //lengthToMarkerUI.SetActive(true);
        }

        if (zoneObj == null)
        {
            zoneObj = GameObject.FindGameObjectWithTag("zone");
            //lengthToMarkerUI.SetActive(true);
        }

        if(zoneInfoObj.activeSelf == false)
        {
            zoneInfoObj.SetActive(true);
        }

        float lengthToMarker = (zoneMarkerObj.transform.position.y - transform.position.y);

        float lengthToZone = zoneObj.transform.localScale.y + zoneObj.GetComponent<zoneControl>().zoneOffset - transform.position.y;

        

        if(sliderRange < lengthToMarker || lengthToMarker < -sliderRange)
        {
            markerSlider.enabled = false;
        }
        else
        {
            markerSlider.enabled = true;
            markerSlider.value = lengthToMarker;
        }

        zoneSlider.value = Mathf.Clamp(lengthToZone, -sliderRange, sliderRange);


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
