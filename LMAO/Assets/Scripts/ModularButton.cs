using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModularButton : MonoBehaviour
{
    public bool delayActive = false;
    public float delayTime;
    private float startTime;
    private Light light;

    public UnityEvent OnPressed;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        if (delayActive)
        {
            if (Time.time - startTime > delayTime)
            {
                delayActive = false;
                GetComponent<Animator>().Play("buttonUp");
                light.enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (delayActive)
        {
            return;
        }

        if (collision.transform.tag == "Player")
        {
            GetComponent<Animator>().Play("buttonDown");
            OnPressed.Invoke();
            delayActive = true; 
            startTime = Time.time;
            light.enabled = false;
            print(light +" is "+ light.intensity.ToString());
        }
    }
}
