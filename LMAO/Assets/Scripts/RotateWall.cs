using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWall : MonoBehaviour
{

    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.value < 0.5f)
        {
            rotateSpeed = -rotateSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(new Vector3(0,0,1) * rotateSpeed * Time.deltaTime);
    }
}
