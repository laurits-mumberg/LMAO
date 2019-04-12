using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;
    
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

    public void DealDamage(float dmg)
    {
        health -= dmg;
        print(health);
    }
}
