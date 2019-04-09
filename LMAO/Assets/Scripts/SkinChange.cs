using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkinChange : MonoBehaviour
{
    public GameObject prewiewBall;
    public BallLook.BallColor yourBallColor;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBallColor()
    {
        int color = animator.GetInteger("ColorChange");
        print(color);
        switch (color)
        {
            case 1:
                yourBallColor = BallLook.BallColor.White;
                prewiewBall.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2:
                yourBallColor = BallLook.BallColor.Red;
                prewiewBall.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 3:
                yourBallColor = BallLook.BallColor.Green;
                prewiewBall.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 4:
                yourBallColor = BallLook.BallColor.Blue;
                prewiewBall.GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
        animator.SetInteger("ColorChange", 0);
    }
}
