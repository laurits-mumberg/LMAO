using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallUI : MonoBehaviourPun
{
    private GameObject canvas;
    private GameObject gameManagerObj;

    //De nye zone ting
    public float sliderRange;
    private GameObject zoneInfoObj;
    private Slider zoneSlider;
    private Slider ballSlider;
    private Slider markerSlider;

    //Gamle zone ting
    private GameObject lengthToMarkerUI;
    private GameObject zoneMarkerObj;
    private GameObject zoneObj;
    private GameObject markerUpArrow;
    private GameObject markerDownArrow;

    //Leave button ting
    private GameObject leaveButton;
    public bool isInMenu;

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

        leaveButton = canvas.transform.Find("ButtonLeave").gameObject;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //UpdateMarkerUI(); <-- Outdated
        UpdateMarkerUI2();

        OpenMenu();
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
            markerSlider.transform.gameObject.SetActive(false);
        }
        else
        {
            markerSlider.transform.gameObject.SetActive(true);
            markerSlider.value = lengthToMarker;
        }

        zoneSlider.value = Mathf.Clamp(lengthToZone, -sliderRange, sliderRange);


    }

    void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInMenu)
            {
                //Åben "menu" lol bare en kanp
                GetComponent<BallControl>().isDisabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                leaveButton.SetActive(true);

                isInMenu = true;
            }
            else
            {
                //Luk "menu"
                GetComponent<BallControl>().isDisabled = false;
                GetComponent<BallControl>().isShooting = false;
                GetComponent<ShotPrediction>().HideDots();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                leaveButton.SetActive(false);

                isInMenu = false;
            }


        }
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
