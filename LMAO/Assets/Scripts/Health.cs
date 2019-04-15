using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviourPun
{
    public float maxHealth = 100f;
    public float health;

    //slider on canvas
    public GameObject canvas;
    public Slider healthbar;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        healthbar = canvas.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health / maxHealth;
        if (health < 0 )
        {
            photonView.RPC("PlayerDie", RpcTarget.All);
        }
    }

    public void DealDamage(float dmg)
    {
        health -= dmg;
        print(health);
    }
    
}
