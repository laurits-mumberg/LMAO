using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenu : MonoBehaviour
{
    private Animator canvasAnimator;

    // Start is called before the first frame update
    void Start()
    {
        canvasAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMenuState(int menuState)
    {
        canvasAnimator.SetInteger("Menu state", menuState);
    }
}
