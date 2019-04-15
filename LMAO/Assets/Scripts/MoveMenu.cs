using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenu : MonoBehaviour
{
    private Animator canvasAnimator;

    //[0] er Skins, [1] er Trails, [2] er emojis
    public GameObject[] curShowcased;

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

    public void ChangeCustomizationOptions(int newCustomize)
    {
        for (int i=0; i<curShowcased.Length; i++)
        curShowcased[i].gameObject.SetActive(false);

        curShowcased[newCustomize].gameObject.SetActive(true);
    } 

}
