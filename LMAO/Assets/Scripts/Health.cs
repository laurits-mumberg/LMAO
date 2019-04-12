using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    
    //slider on canvas
    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health / maxHealth;
        if (health < 0 )
        {
            //die here
        }
    }

    void DealDamage(int dmg)
    {
        health -= dmg;
    }
}
